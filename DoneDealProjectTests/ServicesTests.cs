using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DoneDealProjectTests;

public class ServicesTests
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

    //[Fact]
    //public async Task GetCarModelsAsync_Pass()
    //{
    //    // Given
    //    var carModelService = new CarModelService();

    //    // When
    //    List<CarModel> carModels = await carModelService.GetCarModelsAsync("Ford");

    //    // Then
    //    Assert.NotNull(carModels);
    //    Assert.NotEmpty(carModels);
    //}

    [Fact]
    public async Task GetCarModelsAsync_Fail()
    {
        // Given
        var carModelService = new CarModelService();

        // When
        List<CarModel> carModels = await carModelService.GetCarModelsAsync("Not A Valid Car Model");

        // Then
        Assert.Null(carModels);
    }

    [Fact]
    public async Task GetYearRangeAsync_Success()
    {
        // Given
        var carYearService = new CarYearService();

        // When
        (int fromYear, int toYear) = await carYearService.GetYearRangeAsync();

        // Then
        Assert.True(fromYear > 0 && toYear > 0, "From and To Year are > 0");
        Assert.True(fromYear <= toYear, "From Year is less than or equal to To Year");
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
