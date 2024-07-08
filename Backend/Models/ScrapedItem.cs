using Domain.Items;

namespace Models;

public class ScrapedItem
{
    public Item Details { get; set; }
    public decimal Price { get; set; }
}