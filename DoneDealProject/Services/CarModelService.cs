using DoneDealProject.Model;
using System;

namespace DoneDealProject.Services;

public class CarModelService
{
    public async Task<List<CarModel>> GetCarModelsAsync(string carMake)
    {
        // Read car models from CSV file
        List<CarModel> carModels = await ReadCarModelsFromCSV();

        // Filter car models based on car make
        var filteredModels = carModels.Where(model => model.Make.ToLower() == carMake.ToLower()).ToList();

        if (filteredModels.Any())
        {
            return filteredModels;
        }
        else
        {
            Console.WriteLine($"No car models found for '{carMake}'.");
            return null;
        }
    }

    private async Task<List<CarModel>> ReadCarModelsFromCSV()
    {
        List<CarModel> carModels = new List<CarModel>();

        try
        {
            // Open the Maui asset file "CarModels.csv"
            using (Stream stream = await FileSystem.OpenAppPackageFileAsync("CarModels.csv"))
            using (StreamReader reader = new StreamReader(stream))
            {
                // Read the content of the file
                string content = await reader.ReadToEndAsync();

                // Split the content into lines using newline character '\n'
                string[] lines = content.Split('\n');

                // Parse each line and create CarModel
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 2)
                    {
                        carModels.Add(new CarModel
                        {
                            Make = parts[0].Trim(),
                            Model = parts[1].Trim()
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading CSV file: {ex.Message}");
        }

        return carModels;
    }
}
