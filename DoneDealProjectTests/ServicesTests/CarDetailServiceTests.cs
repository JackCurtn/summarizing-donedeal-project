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

}
