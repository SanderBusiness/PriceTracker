using Domain.Abstract;

namespace Domain.Items;

public class ItemPrice : Entity
{
    public required Item Item { get; set; }
    public decimal Price { get; set; }
}