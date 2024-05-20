using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoneDealProject.Model;

public class CarDetail
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public required string AdvertisementName { get; set; }
    public double Price { get; set; }
    public string EngineSize { get; set; }
    public int Mileage { get; set; }
    public string Location { get; set; }
}
