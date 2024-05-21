using CommunityToolkit.Mvvm.ComponentModel;

namespace DoneDealProject.Model;

public class CarDetail: ObservableObject
{
    public required string Make { get; set; }
    public required string Model { get; set; }
    public required int Year { get; set; }
    public required string AdvertisementName { get; set; }
    public required double Price { get; set; }
    public required string EngineSize { get; set; }
    public required int Mileage { get; set; }
    public required string Location { get; set; }
    public string Url { get; set; }

    private bool _isHighlighted;
    public bool IsHighlighted
    {
        get => _isHighlighted;
        set => SetProperty(ref _isHighlighted, value);
    }
}
