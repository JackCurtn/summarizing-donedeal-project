using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoneDealProject.Model;
using DoneDealProject.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DoneDealProject.ViewModel;

public partial class SummaryViewModel : BaseViewModel
{
    private CarDetailService _carDetailService;
    private ObservableCollection<CarDetail> _allCarDetails;

    [ObservableProperty]
    string selectedCar;

    [ObservableProperty]
    private ObservableCollection<CarDetail> _carDetails;

    [ObservableProperty]
    private ObservableCollection<string> _engineSizes;

    [ObservableProperty]
    private string _selectedEngineSize;

    [ObservableProperty]
    private double _averageMileage;

    [ObservableProperty]
    private double _averageCost;

    public SummaryViewModel(string selectedCarMake, string selectedCarModel, int selectedYear)
    {
        Title = "Summary Page";
        SelectedCar = $"{selectedYear} {selectedCarMake} {selectedCarModel}";

        _carDetailService = new CarDetailService();

        CarDetails = new ObservableCollection<CarDetail>();
        EngineSizes = new ObservableCollection<string>();

        LoadCarDetailsAsync(selectedCarMake, selectedCarModel, selectedYear);
    }

    private async void LoadCarDetailsAsync(string selectedCarMake, string selectedCarModel, int selectedYear)
    {
        var carDetails = await _carDetailService.GetCarDetailsAsync(selectedCarMake, selectedCarModel, selectedYear);
        _allCarDetails = new ObservableCollection<CarDetail>(carDetails);
        if (!_allCarDetails.Any())
        {
            await Shell.Current.DisplayAlert("No Cars Found", "No cars found for the selected make, model, and year.", "OK");
            await Shell.Current.GoToAsync(".."); // .. goes back to the previous page
            return;
        }
        CarDetails = new ObservableCollection<CarDetail>(carDetails);
        EngineSizes = new ObservableCollection<string>(_allCarDetails.Select(c => c.EngineSize).Distinct());
        EngineSizes.Insert(0, "All");
        SelectedEngineSize = "All";
    }

    partial void OnSelectedEngineSizeChanged(string value)
    {
        FilterCarDetails();
    }

    private void FilterCarDetails()
    {
        if (SelectedEngineSize == "All")
        {
            CarDetails = new ObservableCollection<CarDetail>(_allCarDetails);
        }
        else
        {
            CarDetails = new ObservableCollection<CarDetail>(_allCarDetails.Where(c => c.EngineSize == SelectedEngineSize));
        }
        UpdateAverages();

        foreach (var carDetail in CarDetails)
        {
            carDetail.IsHighlighted = carDetail.Price <= AverageCost && carDetail.Mileage <= AverageMileage;
        }
    }

    private void UpdateAverages()
    {
        AverageMileage = CarDetails.Average(c => c.Mileage);
        AverageCost = CarDetails.Average(c => c.Price);
    }
}
