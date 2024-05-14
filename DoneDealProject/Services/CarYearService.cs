using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoneDealProject.Services;

public class CarYearService
{
    const string DONEDEAL_URL = "https://www.donedeal.ie/cars";

    public async Task<(int FromYear, int ToYear)> GetYearRangeAsync(string url = DONEDEAL_URL)
    {
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

                // Extracting year range
                var yearOptions = doc.DocumentNode.SelectNodes("//select[contains(@data-testid, 'from-year')]//option[position() > 1]");
                if (yearOptions != null && yearOptions.Count > 0)
                {
                    var toYear = int.Parse(yearOptions[0].Attributes["value"].Value);
                    var fromYear = int.Parse(yearOptions[yearOptions.Count - 1].Attributes["value"].Value);
                    return (fromYear, toYear);
                }
                else
                {
                    Console.WriteLine("Year options not found.");
                }
            }
            else
            {
                Console.WriteLine($"Response not successful: {response.StatusCode}");
            }
        }

        return (2000, 2024); // Default year range
    }
}
