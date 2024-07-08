using Domain.Items;

namespace BL.Interfaces;

public interface IItemService
{
    Task<List<Item>> GetFromSearch(string searchQuery);
}