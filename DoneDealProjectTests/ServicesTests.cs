using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoneDealProjectTests;

public class ServicesTests { 
    [Fact]
    public async Task GetCarMakesAsync_Success()
    {
        // Given
        var carMakesService = new CarMakesService();

        // When
        List<CarMakes> carMakes = await carMakesService.GetCarMakesAsync();

        // Then
        Assert.NotNull(carMakes);
        Assert.NotEmpty(carMakes);
    }

    [Fact]
    public async Task GetCarMakesAsync_Fail()
    {
        // Given
        var carMakesService = new CarMakesService();

        // When
        List<CarMakes> carMakes = await carMakesService.GetCarMakesAsync("https://www.youtube.com");

        // Then
        Assert.Null(carMakes);
    }
}
