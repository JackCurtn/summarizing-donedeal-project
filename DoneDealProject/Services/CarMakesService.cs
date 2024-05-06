using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using DoneDealProject.Model;
using System.Xml;

namespace DoneDealProject.Services;

public class CarMakesService
{
    const string DONEDEAL_URL = "https://www.donedeal.ie/cars";

    public async Task<List<CarMakes>> GetCarMakesAsync(string url = DONEDEAL_URL) // Make the method public
    {
        string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3";

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string htmlContent = await response.Content.ReadAsStringAsync();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);

                // html element which contains the dropdown items list of each car make
                var dropdownItems = doc.DocumentNode.SelectNodes("//li[contains(@class, 'CustomSelect__ListItem-sc-1j4ks7m-2')]//button//span");
                if (dropdownItems != null)
                {
                    var carMakes = dropdownItems.Select(item => new CarMakes { Name = item.InnerText.Trim() }).ToList();
                    return carMakes;
                }
                else
                {
                    Console.WriteLine("Dropdown items not found.");
                }
            }
            else
            {
                Console.WriteLine($"Failed to fetch the page: {response.StatusCode}");
            }
        }

        return null;
    }
}
