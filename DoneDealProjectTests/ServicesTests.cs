using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
