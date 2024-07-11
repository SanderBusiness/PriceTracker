using System.Net;
using Domain;
using HtmlAgilityPack;
using Scrapers.Abstract;
using Scrapers.Helpers;

namespace Scrapers.Carrefour;

public class CarrefourScraper : DiscoveryScraper<HtmlNode, HtmlDocument>
{
    public override Shop Shop => Shop.Carrefour;
    private HttpClient? _client;
    private readonly CookieContainer _cookieContainer = new();
    protected override async Task<HtmlDocument> FetchPage(string search)
    {
        if (_client == null)
        {
            var handler = new HttpClientHandler()
            {
                CookieContainer = _cookieContainer
            };
            _client = new HttpClient(handler);
            _client.AddUserHeaders();
        }
        var response = await _client.GetAsync($"https://www.carrefour.be/nl/search?q={search}&page=0");
        var pageContent = await response.Content.ReadAsStringAsync();
        var document = new HtmlDocument();
        document.LoadHtml(pageContent);
        return document;
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

    protected override decimal GetPrice(HtmlNode node)
    {
        node =  node.GetSingleNodeWithClass("pricing-wrapper");
        var prices = node.Descendants()
            .Where(e => e.HasClass("value"))
            .Select(e => e.GetAttributeValue("content", "-1"))
            .Where(e => e != "-1")
            //.Select(e => e.Replace(".", ","))
            .Select(decimal.Parse)
            .ToList();
        if (!prices.Any())
            return -1;
        return prices.MinBy(e => e);
    }

    protected override string GetImage(HtmlNode node)
    {
        node =  node.GetSingleNodeWithClass("tile-image");
        return node.GetAttributeValue("src", string.Empty);
    }
}
