using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoneDealProject.Model;

public class CarDetail
{
    public required string AdvertisementName { get; set; }
    public string EngineSize { get; set; }
    public string Mileage { get; set; }
    public string Price { get; set; }
}
