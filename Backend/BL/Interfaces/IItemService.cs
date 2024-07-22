using Domain.Items;

namespace BL.Interfaces;

public interface IItemService
{
    Task<List<Item>> GetFromSearch(string searchQuery);
    Task<Item> Get(Guid id);
}