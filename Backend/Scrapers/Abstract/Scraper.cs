using Microsoft.Extensions.Logging;
using Models;

namespace Scrapers.Abstract;

public abstract class Scraper
{
    protected abstract DiscoveryScraper DiscoveryScraper { get; }
    protected abstract DetailsScraper DetailsScraper { get; }
    
    public async Task<IEnumerable<ScrapedItem?>> Scrape(string searchQuery, ILogger<Scraper> logger)
    {
        var start = DateTimeOffset.Now;
        logger.LogInformation("Discovering items for: {Search}", searchQuery);
        var discoveryResults = await DiscoveryScraper.Discover(searchQuery);
        var detailTasks = new List<Task<ScrapedItem?>>();
        foreach (var discoveredItem in discoveryResults.Where(r => !string.IsNullOrEmpty(r.Url)))
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            detailTasks.Add(DetailsScraper.Scrape(discoveredItem.Url));
        }
        logger.LogInformation("Discovering {Count} items for: {Search}", detailTasks.Count, searchQuery);
        var details = await Task.WhenAll(detailTasks);
        logger.LogInformation("Took {Seconds}s to discover {Count} items for: {Search}", (int) (DateTimeOffset.Now-start).TotalSeconds, details.Length, searchQuery);
        return details;
    }
}