using DoneDealProject.Model;
using DoneDealProject.Services;
using System.Collections.ObjectModel;

namespace DoneDealProject.ViewModel;

public partial class SummaryViewModel : BaseViewModel
{
    private CarDetailService _carDetailService;

    public ObservableCollection<CarDetail> CarDetails { get; set; }

    public SummaryViewModel(string selectedCarMake, string selectedCarModel, int selectedYear)
    {
        Title = "Summary Page";

        _carDetailService = new CarDetailService();

        CarDetails = new ObservableCollection<CarDetail>();

        LoadCarDetailsAsync(selectedCarMake, selectedCarModel, selectedYear);
    }

    private async void LoadCarDetailsAsync(string selectedCarMake, string selectedCarModel, int selectedYear)
    {
        var carDetails = await _carDetailService.GetCarDetailsAsync(selectedCarMake, selectedCarModel, selectedYear);
        CarDetails = new ObservableCollection<CarDetail>(carDetails);
    }
}
