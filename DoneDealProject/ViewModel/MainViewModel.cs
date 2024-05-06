using DoneDealProject.Services;
using DoneDealProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoneDealProject.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    private CarMakesService _carMakesService;

    public ObservableCollection<string> CarMakes { get; private set; }

    public MainViewModel()
    {
        Title = "Front Page";
        _carMakesService = new CarMakesService();
        CarMakes = new ObservableCollection<string>();

        LoadCarMakes();
    }

    // Loads the car makes from the DoneDeal website
    private async void LoadCarMakes()
    {
        List<CarMakes> carMakesList = await _carMakesService.GetCarMakesAsync();
        if (carMakesList != null)
        {
            // Distinct as "All Items" seems to duplicate
            var uniqueCarMakes = carMakesList.Select(carMake => carMake.Name).Distinct();
            foreach (var carMake in uniqueCarMakes)
            {
                CarMakes.Add(carMake);
            }
        }
    }
}

