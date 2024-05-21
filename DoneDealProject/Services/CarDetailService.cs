using HtmlAgilityPack;
using DoneDealProject.Model;

namespace DoneDealProject.Services;

public class CarDetailService
{
    public async Task<List<CarDetail>> GetCarDetailsAsync(string selectedCarMake, string selectedCarModel, int selectedYear)
    {
        int start = 0;
        int pageSize = 30;
        List<CarDetail> carDetails = new List<CarDetail>();

        string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3";

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);

            while (true)
            {
                string url = $"https://www.donedeal.ie/cars/{selectedCarMake}/{selectedCarModel}/{selectedYear}?start={start}";

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string htmlContent = await response.Content.ReadAsStringAsync();
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(htmlContent);

                    var adContainers = doc.DocumentNode.SelectNodes("//li[starts-with(@data-testid, 'listing-card-index')]");

                    if (adContainers != null)
                    {
                        bool lastPage = true;

                        foreach (var adContainer in adContainers)
                        {
                            // Check if ad is a sponsored ad - if it is then skip it
                            if (adContainer.SelectSingleNode(".//p[contains(@class, 'WithHighlightstyled__Highlight')]") != null)
                            {
                                continue;
                            }

                            // This means that there was at least one ad on the page and that was not a sponsored ad
                            lastPage = false;

                            // Extract details from each ad container
                            var adNameNode = adContainer.SelectSingleNode(".//p[contains(@class, 'BasicHeaderstyled__Title')]");
                            var priceNode = adContainer.SelectSingleNode(".//p[contains(@class, 'Pricestyled__Text')]");
                            var urlNode = adContainer.SelectSingleNode(".//a[contains(@class, 'SearchCardstyled__CardLink')]");

                            // 1 - Year, 2 - Engine Size, 3 - Mileage, 4 - Time since Posted, 5 - Location
                            var engineSizeNode = adContainer.SelectSingleNode(".//li[contains(@class, 'BasicHeaderstyled__KeyInfoItem')][2]");
                            var mileageNode = adContainer.SelectSingleNode(".//li[contains(@class, 'BasicHeaderstyled__KeyInfoItem')][3]");
                            var locationNode = adContainer.SelectSingleNode(".//li[contains(@class, 'BasicHeaderstyled__KeyInfoItem')][5]");

                            if (engineSizeNode != null && mileageNode != null && locationNode != null)
                            {
                                string priceString = priceNode.InnerText.Trim().Replace("€", "").Replace(",", "");
                                double price;
                                bool priceParsedSuccessfully = double.TryParse(priceString, out price);
                                int? mileage;

                                // Try parsing mileage only if price is parsed successfully
                                if (priceParsedSuccessfully && (mileage = ParseMileage(mileageNode.InnerText.Trim())).HasValue)
                                {
                                    // Add car detail only if both price and mileage are valid
                                    carDetails.Add(new CarDetail
                                    {
                                        Make = selectedCarMake,
                                        Model = selectedCarModel,
                                        Year = selectedYear,
                                        AdvertisementName = adNameNode.InnerText.Trim(),
                                        Price = price,
                                        EngineSize = ParseEngineSize(engineSizeNode.InnerText.Trim()),
                                        Mileage = mileage.Value,
                                        Location = locationNode.InnerText.Trim(),
                                        Url = "www.donedeal.ie" + urlNode.OuterHtml.Split('"')[1]
                                    });
                                }
                            }
                        }

                        // If only sponsored ads were found, break the while loop
                        if (lastPage)
                        {
                            Console.WriteLine("No more ads to scrape.");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No more ads to scrape.");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"Response not successful: {response.StatusCode}");
                    break;
                }

                start += pageSize;
            }
        }

        return carDetails;
    }

    // int? as it can be null if parsing fails
    private int? ParseMileage(string mileageString)
    {
        int mileage;

        // Split the string into parts and extract mileageValue
        string[] parts = mileageString.Trim().Split(' ');
        string mileageValue = parts[0].Replace(",", "");

        // First part should be the mileage value
        if (int.TryParse(mileageValue, out mileage))
        {
            if (parts.Length > 1 && parts[1].ToLower() == "km")
            {
                // Convert km to mi
                mileage = (int)Math.Round(mileage * 0.621371);
            }
            return mileage;
        }

        return null;
    }

    // Something EngineSize appears as "X Petrol Hybrid" so transform to "X Hybrid"
    private string ParseEngineSize(string engineSizeString)
    {
        if (engineSizeString.ToLower().Contains("hybrid"))
        {
            string[] parts = engineSizeString.Split(' ');
            return $"{parts[0]} Hybrid";
        }
        return engineSizeString;
    }
}
