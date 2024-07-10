using BL.Interfaces;
using DAL.EF;
using Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BL.Implementations;

public class ItemService(IScraperService scraperService, ApplicationContext db, ILogger<ItemService> logger) : IItemService
{
    public async Task<List<Item>> GetFromSearch(string searchQuery)
    {
        var count = await GetItemSearchQuery(searchQuery).CountAsync();
        logger.LogInformation("Found {Count} items for search: {Query}", count, searchQuery);
        if (count == 0)
            await scraperService.Discover(searchQuery);
        return await GetItemSearchQuery(searchQuery).ToListAsync();
    }

    private IQueryable<Item> GetItemSearchQuery(string searchQuery)
    {
        var query = searchQuery.ToLower();
        return db.Items
            .Include(e => e.PriceHistory)
            .Where(e => e.SearchQueries
                .Any(q => q.Search.ToLower().Contains(query)
                || q.Item.Title.ToLower().Contains(query)));
    }
}