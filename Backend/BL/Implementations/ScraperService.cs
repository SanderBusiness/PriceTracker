using BL.Interfaces;
using DAL.EF;
using Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Scrapers.Abstract;
using Scrapers.Carrefour;

namespace BL.Implementations;

public class ScraperService(ILogger<ScraperService> logger, ApplicationContext db, IEnumerable<IDiscoveryScraper> scrapers) : IScraperService
{
    private readonly DateTimeOffset _serviceCreatedOn = DateTimeOffset.Now;
    private List<IDiscoveryScraper> _scrapers => scrapers.ToList();
    
    public async Task Discover(string search)
    {
        var start = DateTimeOffset.Now;
        logger.LogInformation("Discovering items for: {Search} (service created on: {Date})", search, _serviceCreatedOn.ToString());
        var discoveryTasks = new List<Task<List<DiscoveredItem>>>();
        _scrapers.ForEach(s => discoveryTasks.Add(s.Discover(search)));
        var discoveryResultsSplit = await Task.WhenAll(discoveryTasks);
        var discoveryResults = discoveryResultsSplit.SelectMany(e => e).ToList();
        var upsertTasks = new List<Task>();
        discoveryResults.ForEach(e => upsertTasks.Add(Upsert(e, search)));
        logger.LogInformation("Scraped {Count} items in {Seconds}s for search: {Query}", upsertTasks.Count, (DateTimeOffset.Now-start).TotalSeconds, search);
        await Task.WhenAll(upsertTasks);
    }

    private async Task Upsert(DiscoveredItem? item, string searchQuery)
    {
        if (item == null || string.IsNullOrEmpty(item.Title) || string.IsNullOrEmpty(item.Image) || string.IsNullOrEmpty(item.Url))
            return;
        var entity = await db.Items.FirstOrDefaultAsync(e => e.Url == item.Url);
        if (entity == null)
        {
            entity = new Item()
            {
                Title = item.Title,
                Image = item.Image,
                Shop = item.Shop,
                Url = item.Url,
            };
            db.Items.Add(entity);
        }
        else
        {
            entity.Title = item.Title;
            entity.Image = item.Image;
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
}
