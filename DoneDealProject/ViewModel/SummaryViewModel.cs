using CommunityToolkit.Mvvm.ComponentModel;
using DoneDealProject.Model;
using DoneDealProject.Services;
using System.Collections.ObjectModel;

namespace DoneDealProject.ViewModel;

public partial class SummaryViewModel : BaseViewModel
{
    private CarDetailService _carDetailService;
    private ObservableCollection<CarDetail> _allCarDetails;

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

        _carDetailService = new CarDetailService();

        CarDetails = new ObservableCollection<CarDetail>();
        EngineSizes = new ObservableCollection<string>();

        LoadCarDetailsAsync(selectedCarMake, selectedCarModel, selectedYear);
    }

    private async void LoadCarDetailsAsync(string selectedCarMake, string selectedCarModel, int selectedYear)
    {
        var carDetails = await _carDetailService.GetCarDetailsAsync(selectedCarMake, selectedCarModel, selectedYear);
        _allCarDetails = new ObservableCollection<CarDetail>(carDetails);
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
    }

    private void UpdateAverages()
    {
        if (CarDetails.Count > 0)
        {
            AverageMileage = CarDetails.Average(c => c.Mileage);
            AverageCost = CarDetails.Average(c => c.Price);
        }
        else
        {
            AverageMileage = 0;
            AverageCost = 0;
        }
    }
}
