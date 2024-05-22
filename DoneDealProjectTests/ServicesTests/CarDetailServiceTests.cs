using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoneDealProjectTests.ServicesTests;
public class CarDetailServiceTests
{
    [Fact]
    public async Task GetCarDetailsAsync_Success()
    {
        // Given
        var carDetailService = new CarDetailService();
        string selectedCarMake = "Toyota";
        string selectedCarModel = "Corolla";
        int selectedYear = 2016;

        // When
        List<CarDetail> carDetails = await carDetailService.GetCarDetailsAsync(selectedCarMake, selectedCarModel, selectedYear);

        // Then
        Assert.NotEmpty(carDetails);
    }

    [Fact]
    public async Task GetCarDetailsAsync_InvalidYear()
    {
        // Given
        var carDetailService = new CarDetailService();
        string selectedCarMake = "Toyota";
        string selectedCarModel = "Corolla";
        int selectedYear = 1970;

        // When
        List<CarDetail> carDetails = await carDetailService.GetCarDetailsAsync(selectedCarMake, selectedCarModel, selectedYear);

        // Then
        Assert.Empty(carDetails);
    }

    [Fact]
    public async Task GetCarDetailsAsync_InvalidModel()
    {
        // Given
        var carDetailService = new CarDetailService();
        string selectedCarMake = "Toyota";
        string selectedCarModel = "Focus";
        int selectedYear = 2016;

        // When
        List<CarDetail> carDetails = await carDetailService.GetCarDetailsAsync(selectedCarMake, selectedCarModel, selectedYear);

        // Then
        Assert.Empty(carDetails);
    }

    [Fact]
    public async Task GetCarDetailsAsync_InvalidMake()
    {
        // Given
        var carDetailService = new CarDetailService();
        string selectedCarMake = "Tesco";
        string selectedCarModel = "Corolla";
        int selectedYear = 2016;

        // When
        List<CarDetail> carDetails = await carDetailService.GetCarDetailsAsync(selectedCarMake, selectedCarModel, selectedYear);

        // Then
        Assert.Empty(carDetails);
    }
}
