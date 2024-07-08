using Domain;
using Domain.Items;
using HtmlAgilityPack;
using Models;

namespace Scrapers.Abstract;

public abstract class DiscoveryScraper
{
    protected abstract Shop Shop { get; }
    protected abstract Task<string> FetchPage(string search);
    protected abstract IEnumerable<HtmlNode> GetItems(HtmlDocument document);
    protected abstract string GetTitle(HtmlNode node);
    protected abstract string GetUrl(HtmlNode node);
    
    /// <param name="search">Filter to find specific items</param>
    /// <returns>Url of possible items</returns>
    public async Task<List<DiscoveredItem>> Discover(string search)
    {
        var pageContent = await FetchPage(search);
        var document = new HtmlDocument();
        document.LoadHtml(pageContent);

        var itemNodes = GetItems(document);

        return itemNodes.Select(itemNode => new DiscoveredItem()
            {
                Title = GetTitle(itemNode),
                Url = GetUrl(itemNode),
                Shop = Shop,
            })
            .Where(item => !string.IsNullOrEmpty(item.Title))
            .Where(item => !string.IsNullOrEmpty(item.Url))
            .ToList();
    }
}