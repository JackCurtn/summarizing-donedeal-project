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

        // When
        List<CarMake> carMakes = await carMakesService.GetCarMakesAsync("https://www.youtube.com");

        // Then
        Assert.Null(carMakes);
    }

    [Fact]
    public async Task GetCarMakesAsync_InvalidURL()
    {
        // Given
        var carMakesService = new CarMakeService();

        // When and Then
        await Assert.ThrowsAsync<InvalidOperationException>(() => carMakesService.GetCarMakesAsync("Not A Valid URL"));
    }
}
