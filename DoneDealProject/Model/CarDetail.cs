using CommunityToolkit.Mvvm.ComponentModel;

namespace DoneDealProject.Model;

public class CarDetail : ObservableObject
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string AdvertisementName { get; set; }
    public double Price { get; set; }
    public string EngineSize { get; set; }
    public int Mileage { get; set; }
    public string Location { get; set; }
    public string Url { get; set; }

    private bool _isHighlighted;
    public bool IsHighlighted
    {
        get => _isHighlighted;
        set => SetProperty(ref _isHighlighted, value);
    }
}
