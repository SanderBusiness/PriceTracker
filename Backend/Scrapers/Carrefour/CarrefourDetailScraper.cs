using System.Net;
using Domain;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Scrapers.Abstract;
using Scrapers.Helpers;

namespace Scrapers.Carrefour;

public class CarrefourDetailScraper(HttpClient client, ILoggerFactory loggerFactory) : DetailsScraper(loggerFactory)
{
    protected override Shop Shop => Shop.Carrefour;
    protected override async Task<string> FetchPage(string url)
    {
        var response = await client.GetAsync(url);
        if (response.StatusCode == HttpStatusCode.TooManyRequests)
            return string.Empty;
        return await response.Content.ReadAsStringAsync();
    }

    protected override string GetTitle(HtmlDocument document)
    {
        var node = document.GetNodesWithClass("product-name").FirstOrDefault();
        return node == null 
            ? string.Empty 
            : node.InnerText;
    }

    protected override string GetDescription(HtmlDocument document)
    {
        var node = document.GetNodesWithClass("attribute-meer-details").FirstOrDefault();
        return node == null 
            ? string.Empty 
            : node.InnerText.Replace("\nMeer details\n", string.Empty);
    }

    protected override decimal GetPrice(HtmlDocument document)
    {
        var node = document.GetNodesWithClass("pricing-wrapper")
            .FirstOrDefault()?
            .GetSingleNodeWithClass("value");
        return node?.GetAttributeValue("content", (decimal) -1) ?? -1;
    }

    protected override string GetImage(HtmlDocument document)
    {
        var node = document.GetNodesWithClass("zoom-overlay__mainImage").FirstOrDefault();
        return node == null 
            ? string.Empty 
            : node.GetAttributeValue("src", string.Empty);
    }
}
