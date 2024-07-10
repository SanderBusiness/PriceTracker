using Domain;
using Models;

namespace Scrapers.Abstract;

public abstract class DiscoveryScraper<TP, TD> : IDiscoveryScraper
{
    public abstract Shop Shop { get; }
    protected abstract Task<TD> FetchPage(string search);
    protected abstract IEnumerable<TP> GetItems(TD document);
    protected abstract string GetTitle(TP node);
    protected abstract string GetUrl(TP node);
    protected abstract decimal GetPrice(TP node);
    protected abstract string GetImage(TP node);

    /// <param name="search">Filter to find specific items</param>
    /// <returns>Url of possible items</returns>
    public async Task<List<DiscoveredItem>> Discover(string search)
    {
        var document = await FetchPage(search);

        var itemNodes = GetItems(document);

        return itemNodes.Select(itemNode => new DiscoveredItem()
            {
                Title = GetTitle(itemNode),
                Url = GetUrl(itemNode),
                Price = GetPrice(itemNode),
                Image = GetImage(itemNode),
                Shop = Shop,
            })
            .Where(item => !string.IsNullOrEmpty(item.Title))
            .Where(item => !string.IsNullOrEmpty(item.Url))
            .ToList();
    }
}