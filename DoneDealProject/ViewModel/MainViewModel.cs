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
    private CarMakeService _carMakesService;
    private CarModelService _carModelService;

    public ObservableCollection<string> CarMakes { get; private set; }
    public ObservableCollection<string> CarModels { get; private set; }

    private string _selectedCarMake;
    public string SelectedCarMake
    {
        get { return _selectedCarMake; }
        set
        {
            if (_selectedCarMake != value)
            {
                _selectedCarMake = value;
                OnPropertyChanged(nameof(SelectedCarMake));
                LoadCarModels();
            }
        }
    }

    public MainViewModel()
    {
        Title = "Front Page";
        _carMakesService = new CarMakeService();
        _carModelService = new CarModelService();
        CarMakes = new ObservableCollection<string>();
        CarModels = new ObservableCollection<string>();

        LoadCarMakes();
    }

    // Loads the car makes from the DoneDeal website
    private async void LoadCarMakes()
    {
        List<CarMake> carMakesList = await _carMakesService.GetCarMakesAsync();
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

    private async void LoadCarModels()
    {
        if (!string.IsNullOrEmpty(SelectedCarMake))
        {
            CarModels.Clear();
            List<CarModel> carModelsList = await _carModelService.GetCarModelsAsync(SelectedCarMake);
            if (carModelsList != null)
            {
                foreach (var carModel in carModelsList)
                {
                    CarModels.Add(carModel.Model);
                }
            }
        }
    }
}

