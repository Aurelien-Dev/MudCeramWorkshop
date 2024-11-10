using System.Globalization;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using MudBlazor;
using PuppeteerSharp;

namespace MudCeramWorkshop.Client.Routes;

public static class ThroneAndLibertyRoutes
{
    public static IEndpointConventionBuilder MapTaLPageRoute(this IEndpointRouteBuilder endpoints)
    {
        // array of TaLEvent
        var events = new List<TaLEvent>();

        var apiGroup = endpoints.MapGroup("/api");

        apiGroup.MapGet("todayEvents", async () =>
            {
                var content = await GetHtmlPage();
                var bodyContent = GetBody(content);
                var doc = new HtmlDocument();
                doc.LoadHtml(bodyContent);

                var matchingNodes =
                    doc.DocumentNode.SelectNodes(
                        "//img[contains(@src, 'DynamicEvent') or contains(@src, 'FieldBossEvent') or contains(@src, 'WorldBossEvent')]/ancestor::div[4]");
                if (matchingNodes != null)
                {
                    foreach (var node in matchingNodes)
                    {
                        (DateTime time, string url, string type) = GetTimeAndUrl(node.OuterHtml);
                        events.Add(new TaLEvent() { Time = time, IconUrl = url, Type = type });
                    }
                }

                return events;
            })
            .WithOpenApi();

        return apiGroup;
    }


    private static async Task<string> GetHtmlPage()
    {
        await new BrowserFetcher().DownloadAsync();
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
        await using var page = await browser.NewPageAsync();

        var customHeaders = new Dictionary<string, string>
        {
            {
                "User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Safari/537.36"
            },
            { "Referer", "https://www.google.com/" }
        };

        await page.SetExtraHttpHeadersAsync(customHeaders);
        //create object date time now format yyyy-MM-dd
        var date = DateTime.Now.ToString("yyyy-MM-dd");

        await page.GoToAsync(
            $"https://questlog.gg/throne-and-liberty/fr/event-calendar?levelMax=50&date={date}&regionId=ko&serverId=23");
        await page.WaitForSelectorAsync("#__nuxt > div > main > div.container-lg.space-y-3.py-3");

        return await page.GetContentAsync();
    }

    private static string GetBody(string htmlContent)
    {
        var bodyRegex = new Regex(@"<body.*?>.*<\/body>", RegexOptions.Singleline);
        var match = bodyRegex.Match(htmlContent);

        if (!match.Success)
            return string.Empty;

        return match.Value;
    }

    private static (DateTime, string, string) GetTimeAndUrl(string htmlContent)
    {
        // Charger le contenu HTML
        var doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);

        // Extraire la valeur "02:00"
        var timeNode =
            doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'w-24') and contains(@class, 'text-center')]");
        string timeValue = timeNode?.InnerText.Trim();

        // Extraire l'URL de l'image
        var imageNode = doc.DocumentNode.SelectSingleNode(
            "//img[contains(@src, 'DynamicEvent') or contains(@src, 'FieldBossEvent') or contains(@src, 'WorldBossEvent')]");
        string imageUrl = imageNode?.GetAttributeValue("src", string.Empty);

        //convert timeValue to datetime now with timeValue
        var time = DateTime.ParseExact(timeValue, "HH:mm", CultureInfo.InvariantCulture);
        
        string type = "";
        //verify if n the url we have DynamicEvent, FieldBossEvent or WorldBossEvent and return this type
        if (imageUrl.Contains("DynamicEvent"))
        {
            type = "DynamicEvent";
        }
        else if (imageUrl.Contains("FieldBossEvent"))
        {
            type = "FieldBossEvent";
        }
        else if (imageUrl.Contains("WorldBossEvent"))
        {
            type = "WorldBossEvent";
        }
        
        
        return (time, imageUrl, type);
    }
}

public class TaLEvent
{
    public DateTime Time { get; set; }
    public string IconUrl { get; set; }
    public string Type { get; set; }
}