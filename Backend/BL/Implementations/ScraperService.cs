using BL.Helpers;
using BL.Interfaces;
using DAL.EF;
using Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace BL.Implementations;

public class ScraperService(ILogger<ScraperService> logger, ApplicationContext db) : IScraperService
{
    public async Task Discover(string search)
    {
        var start = DateTimeOffset.Now;
        logger.LogInformation("Discovering items for: {Search}", search);
        var scrapers = ScraperFactory.DiscoveryScrapers();
        var discoveryTasks = new List<Task<List<DiscoveredItem>>>();
        scrapers.ForEach(s => discoveryTasks.Add(s.Discover(search)));
        var discoveryResults = await Task.WhenAll(discoveryTasks);
        var detailTasks = new List<Task<ScrapedItem?>>();
        discoveryResults.ToList()
            .ForEach(discoveredItem 
                => discoveredItem
                    .ForEach(item 
                        => detailTasks.Add(FetchDetails(item))));
        logger.LogInformation("Discovering {Count} items for: {Search}", detailTasks.Count(), search);
        var details = await Task.WhenAll(detailTasks);
        var upsertTasks = new List<Task>();
        details.ToList().ForEach(i => upsertTasks.Add(Upsert(i, search)));
        await Task.WhenAll(upsertTasks);
        logger.LogInformation("Took {Seconds}s to discover {Count} items for: {Search}", (int) (DateTimeOffset.Now-start).TotalSeconds, upsertTasks.Count, search);
    }

    private async Task Upsert(ScrapedItem? item, string searchQuery)
    {
        if (item == null || string.IsNullOrEmpty(item.Details.Title) || string.IsNullOrEmpty(item.Details.Image) || string.IsNullOrEmpty(item.Details.Url))
            return;
        var entity = await db.Items.FirstOrDefaultAsync(e => e.Url == item.Details.Url);
        if (entity == null)
        {
            entity = item.Details;
            db.Items.Add(entity);
        }
        else
        {
            entity.Title = item.Details.Title;
            entity.Description = item.Details.Description;
            entity.Image = item.Details.Image;
        }
        entity.SearchQueries.Add(new ItemSearch()
        {
            Search = searchQuery,
            Item = entity,
        });
        if (item.Price > 0)
            entity.PriceHistory.Add(new ItemPrice()
            {
                Item = entity,
                Price = item.Price,
            });
        await db.SaveChangesAsync();
    }

    private async Task<ScrapedItem?> FetchDetails(DiscoveredItem item)
    {
        var scrapers = ScraperFactory.DetailScrapers();
        var scraper = scrapers.FirstOrDefault(s => s.Shop == item.Shop);
        if (scraper == null)
            return null;
        return await scraper.Scrape(item.Url);
    }
}
