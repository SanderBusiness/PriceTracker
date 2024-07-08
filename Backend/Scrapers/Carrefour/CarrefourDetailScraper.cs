using Domain;
using HtmlAgilityPack;
using Scrapers.Abstract;
using Scrapers.Helpers;

namespace Scrapers.Carrefour;

public class CarrefourDetailScraper : DetailsScraper
{
    public override Shop Shop => Shop.Carrefour;
    protected override async Task<string> FetchPage(string url)
    {
        var client = ScraperHttpClient.Get();
        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }

    protected override string GetTitle(HtmlDocument document)
    {
        var node = document
            .DocumentNode
            .Descendants()
            .FirstOrDefault(e => e.HasClass("product-name"));
        return node == null 
            ? string.Empty 
            : node.InnerText;
    }

    protected override string GetDescription(HtmlDocument document)
    {
        var node = document
            .DocumentNode
            .Descendants()
            .FirstOrDefault(e => e.HasClass("attribute-meer-details"));
        return node == null 
            ? string.Empty 
            : node.InnerText.Replace("\nMeer details\n", string.Empty);
    }

    protected override decimal GetPrice(HtmlDocument document)
    {
        var node = document
            .DocumentNode
            .Descendants()
            .FirstOrDefault(e => e.HasClass("pricing-wrapper"))?
            .Descendants()
            .FirstOrDefault(e => e.HasClass("value"));
        if (node == null)
            return -1;
        return node.GetAttributeValue("content", (decimal) -1);
    }

    protected override string GetImage(HtmlDocument document)
    {
        var node = document
            .DocumentNode
            .Descendants()
            .FirstOrDefault(e => e.HasClass("zoom-overlay__mainImage"));
        return node == null 
            ? string.Empty 
            : node.GetAttributeValue("src", string.Empty);
    }
}