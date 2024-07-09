using BL.Interfaces;
using DAL.EF;
using Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Scrapers.Abstract;
using Scrapers.Carrefour;

namespace BL.Implementations;

public class ScraperService(ILogger<ScraperService> logger, ApplicationContext db, ILoggerFactory loggerFactory) : IScraperService
{
    private readonly List<Scraper> _scrapers = [new CarrefourScraper(loggerFactory)];
    
    public async Task Discover(string search)
    {
        var start = DateTimeOffset.Now;
        logger.LogInformation("Discovering items for: {Search}", search);
        var resultTasks = new List<Task<IEnumerable<ScrapedItem?>>>();
        _scrapers.ForEach(s => resultTasks.Add(s.Scrape(search, loggerFactory.CreateLogger<Scraper>())));
        var results = await Task.WhenAll(resultTasks);
        var resultsCombined = results.SelectMany(i => i).Cast<ScrapedItem>().ToList();
        var upsertTasks = new List<Task>();
        resultsCombined.ForEach(e => upsertTasks.Add(Upsert(e, search)));
        logger.LogInformation("Scraped {Count} items in {Seconds}s for search: {Query}", upsertTasks.Count, (DateTimeOffset.Now-start).TotalSeconds, search);
        await Task.WhenAll(upsertTasks);
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
}
