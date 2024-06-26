﻿namespace DoneDealProjectTests.ServicesTests;

public class CarModelServiceTests
{

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
        var carModel = "Not A Valid Car Model";

        // When
        List<CarModel> carModels = await carModelService.GetCarModelsAsync(carModel);

        // Then
        Assert.Null(carModels);
    }
}
