namespace DoneDealProjectTests.ServicesTests;

public class CarYearServiceTests
{
    [Fact]
    public async Task GetYearRangeAsync_Success()
    {
        // Given
        var carYearService = new CarYearService();

        // When
        (int fromYear, int toYear) = await carYearService.GetYearRangeAsync();

        // Then
        Assert.True(fromYear > 0 && toYear > 0, "From and To Year are > 0");
        Assert.True(fromYear <= toYear, "FromYear is less than or equal to ToYear");
    }

    [Fact]
    public async Task GetYearRangeAsync_Fail()
    {
        // Given
        var carYearService = new CarYearService();

        // When
        (int fromYear, int toYear) = await carYearService.GetYearRangeAsync("https://www.youtube.com");

        // Then (Default values)
        Assert.Equal(2000, fromYear);
        Assert.Equal(2024, toYear);
    }

    [Fact]
    public async Task GetYearRangeAsync_InvalidURL()
    {
        // Given
        var carYearService = new CarYearService();

        // When and Then
        await Assert.ThrowsAsync<InvalidOperationException>(() => carYearService.GetYearRangeAsync("Not A Valid URL"));
    }
}
