using Domain.Items;

namespace Models;

public class DiscoveredItem : Item
{
    public decimal Price { get; init; } = -1;
}