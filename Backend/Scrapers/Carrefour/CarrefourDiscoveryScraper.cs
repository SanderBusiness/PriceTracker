using Domain;
using HtmlAgilityPack;
using Scrapers.Abstract;
using Scrapers.Helpers;

namespace Scrapers.Carrefour;

public class CarrefourDiscoveryScraper : DiscoveryScraper
{
    protected override Shop Shop => Shop.Carrefour;
    protected override async Task<string> FetchPage(string search)
    {
        var client = ScraperHttpClient.Get();
        var response = await client.GetAsync($"https://www.carrefour.be/nl/search?q={search}&page=0");
        return await response.Content.ReadAsStringAsync();
    }

    protected override IEnumerable<HtmlNode> GetItems(HtmlDocument document)
    {
        return document.GetNodesWithClass("js-product");
    }

    protected override string GetTitle(HtmlNode node)
    {
        return node.GetSingleNodeWithClass("desktop-name").InnerText;
    }

    protected override string GetUrl(HtmlNode node)
    {
        node =  node.GetSingleNodeWithClass("js-product-tile-gtm");
        node = node.GetSingleNodeWithClass("image-container");
        var page = node.ChildNodes[1].GetAttributeValue("href", string.Empty);
        return page == null 
            ? string.Empty 
            : $"https://www.carrefour.be/nl/{page}";
    }
}