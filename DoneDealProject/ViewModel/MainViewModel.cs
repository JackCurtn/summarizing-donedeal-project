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
    private CarYearService _carYearService;
    private CarModelService _carModelService;

    public ObservableCollection<string> CarMakes { get; set; }
    public ObservableCollection<int> YearRange { get; set; }
    public ObservableCollection<string> CarModels { get; set; }

    public int FromYear { get; set; }
    public int ToYear { get; set; }

    private int _selectedYear;
    public int SelectedYear
    {
        get { return _selectedYear; }
        set
        {
            if (_selectedYear != value)
            {
                _selectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
            }
        }
    }

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
        _carYearService = new CarYearService();

        CarMakes = new ObservableCollection<string>();
        CarModels = new ObservableCollection<string>();
        YearRange = new ObservableCollection<int>();

        LoadCarMakes();
        LoadCarYearRange();
    }

    // Loads the car makes from the DoneDeal website
    private async void LoadCarMakes()
    {
        List<CarMake> carMakesList = await _carMakesService.GetCarMakesAsync();
        if (carMakesList != null)
        {
            // Distinct as Popular Models duplicate 
            var uniqueCarMakes = carMakesList.Select(carMake => carMake.Name).Distinct().OrderBy(carMake => carMake); ;
            foreach (var carMake in uniqueCarMakes)
            {
                CarMakes.Add(carMake);
            }

            // Remove the All Makes option
            CarMakes.Remove("All Makes");
        }
    }

    private async void LoadCarYearRange()
    {
        (FromYear, ToYear) = await _carYearService.GetYearRangeAsync();

        // Populate YearRange with years between FromYear and ToYear
        YearRange.Clear();
        for (int year = FromYear; year <= ToYear; year++)
        {
            YearRange.Add(year);
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
