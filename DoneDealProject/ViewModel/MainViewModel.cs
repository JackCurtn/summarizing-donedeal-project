using DoneDealProject.Services;
using DoneDealProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DoneDealProject.View;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DoneDealProject.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private CarMakeService _carMakesService;
    private CarYearService _carYearService;
    private CarModelService _carModelService;

    [ObservableProperty]
    string title;

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
                UpdateIsSummaryPageEnabled();
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
                UpdateIsSummaryPageEnabled();
            }
        }
    }

    private string _selectedCarModel;
    public string SelectedCarModel
    {
        get { return _selectedCarModel; }
        set
        {
            if (_selectedCarModel != value)
            {
                _selectedCarModel = value;
                OnPropertyChanged(nameof(SelectedCarModel));
                UpdateIsSummaryPageEnabled();
            }
        }
    }

    private bool _isSummaryPageEnabled;
    public bool IsSummaryPageEnabled
    {
        get { return _isSummaryPageEnabled; }
        set
        {
            if (_isSummaryPageEnabled != value)
            {
                _isSummaryPageEnabled = value;
                OnPropertyChanged(nameof(IsSummaryPageEnabled));
            }
        }
    }

    public ICommand NavigateToSummaryPageCommand { get; }

    public MainViewModel()
    {
        Title = "Front Page";

        _carMakesService = new CarMakeService();
        _carModelService = new CarModelService();
        _carYearService = new CarYearService();

        CarMakes = new ObservableCollection<string>();
        CarModels = new ObservableCollection<string>();
        YearRange = new ObservableCollection<int>();

        NavigateToSummaryPageCommand = new Command(ExecuteNavigateToSummaryPageCommand);

        LoadCarMakes();
        LoadCarYearRange();
    }

    private void UpdateIsSummaryPageEnabled()
    {
        IsSummaryPageEnabled = !string.IsNullOrEmpty(SelectedCarMake) &&
                               !string.IsNullOrEmpty(SelectedCarModel) &&
                               SelectedYear != 0;
    }

    private async void ExecuteNavigateToSummaryPageCommand()
    {
        var viewModel = new SummaryViewModel(SelectedCarMake, SelectedCarModel, SelectedYear);
        await Application.Current.MainPage.Navigation.PushAsync(new SummaryPage(viewModel));
    }

    // Loads the car makes from the DoneDeal website
    private async void LoadCarMakes()
    {
        List<CarMake> carMakesList = await _carMakesService.GetCarMakesAsync();
        if (carMakesList != null)
        {
            // Distinct as Popular Models duplicate 
            var uniqueCarMakes = carMakesList.Select(carMake => carMake.Make).Distinct().OrderBy(carMake => carMake); ;
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
                // Sort car models alphabetically by the Model property
                var sortedCarModels = carModelsList.OrderBy(carModel => carModel.Model);
                foreach (var carModel in sortedCarModels)
                {
                    CarModels.Add(carModel.Model);
                }
            }
        }
    }
}
