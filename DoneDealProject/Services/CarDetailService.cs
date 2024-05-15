using HtmlAgilityPack;
using DoneDealProject.Model;

namespace DoneDealProject.Services;

public class CarDetailService
{
    public const string DONEDEAL_URL = "https://www.donedeal.ie/cars";

    public async Task<List<CarDetail>> GetCarDetailsAsync(string selectedCarMake, string selectedCarModel, int selectedYear)
    {
        string url = $"{DONEDEAL_URL}/{selectedCarMake}/{selectedCarModel}/{selectedYear}";
        string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3";

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string htmlContent = await response.Content.ReadAsStringAsync();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);

                // Find the ul element containing the advertisement containers
                var adList = doc.DocumentNode.SelectSingleNode("//ul[@class='Listingsstyled__List-sc-bgdxad-0' and @data-testid='card-list']");
                var adContainers = doc.DocumentNode.SelectNodes("//ul[@class='Listingsstyled__List-sc-bgdxad-0' and @data-testid='card-list']");
                if (adContainers != null)
                {
                    List<CarDetail> carDetailsList = new List<CarDetail>();

                    foreach (var adContainer in adContainers)
                    {
                        var adNameNode = adContainer.SelectSingleNode(".//p[contains(@class, 'BasicHeaderstyled__Title')]");
                        var engineSizeNode = adContainer.SelectNodes(".//li[contains(@class, 'BasicHeaderstyled__KeyInfoItem')]");
                        var mileageNode = engineSizeNode?[1];
                        var priceNode = adContainer.SelectSingleNode(".//p[contains(@class, 'Pricestyled__Text')]");

                        if (adNameNode != null && engineSizeNode != null && mileageNode != null && priceNode != null)
                        {
                            CarDetail carDetail = new CarDetail
                            {
                                AdvertisementName = adNameNode.InnerText.Trim(),
                                EngineSize = engineSizeNode[1].InnerText.Trim(),
                                Mileage = engineSizeNode[2].InnerText.Trim(),
                                Price = priceNode.InnerText.Trim()
                            };
                            carDetailsList.Add(carDetail);
                        }
                    }

                    return carDetailsList;
                }
                else
                {
                    Console.WriteLine("Ad containers not found.");
                }
            }
            else
            {
                Console.WriteLine($"Response not successful: : {response.StatusCode}");
            }
        }

        return null;
    }
}
