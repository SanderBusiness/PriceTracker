using BL.Interfaces;
using DAL.EF;
using Domain.Items;
using Microsoft.EntityFrameworkCore;

namespace BL.Implementations;

public class ItemService(IScraperService scraperService, ApplicationContext db) : IItemService
{
    public async Task<List<Item>> GetFromSearch(string searchQuery)
    {
        var count = await GetItemSearchQuery(searchQuery).CountAsync();
        if (count < 5)
            await scraperService.Discover(searchQuery);
        return await GetItemSearchQuery(searchQuery).ToListAsync();
    }

    private IQueryable<Item> GetItemSearchQuery(string searchQuery)
    {
        return db.Items
            .Include(e => e.PriceHistory)
            .Where(e => e.SearchQueries.Any(q => q.Search.ToLower().Contains(searchQuery.ToLower())));
    }
}