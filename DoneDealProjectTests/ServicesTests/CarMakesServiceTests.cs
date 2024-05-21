namespace DoneDealProjectTests.ServicesTests;

public class CarMakesServiceTests
{
    [Fact]
    public async Task GetCarMakesAsync_Success()
    {
        // Given
        var carMakesService = new CarMakeService();

        // When
        List<CarMake> carMakes = await carMakesService.GetCarMakesAsync();

        // Then
        Assert.NotNull(carMakes);
        Assert.NotEmpty(carMakes);
    }

    [Fact]
    public async Task GetCarMakesAsync_Fail()
    {
        // Given
        var carMakesService = new CarMakeService();
        string url = "https://www.youtube.com";

        // When
        List<CarMake> carMakes = await carMakesService.GetCarMakesAsync(url);

        // Then
        Assert.Null(carMakes);
    }

    [Fact]
    public async Task GetCarMakesAsync_InvalidURL()
    {
        // Given
        var carMakesService = new CarMakeService();
        string url = "Not A Valid URL";

        // When and Then
        await Assert.ThrowsAsync<InvalidOperationException>(() => carMakesService.GetCarMakesAsync(url));
    }
}
