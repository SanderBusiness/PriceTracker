using Domain.Abstract;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Domain.Items;

public class Item : Entity
{
    [JsonConverter(typeof(StringEnumConverter))]
    public required Shop Shop { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Url { get; set; }
    public required string Image { get; set; }

    public ICollection<ItemPrice> PriceHistory { get; set; } = new HashSet<ItemPrice>();
    public ICollection<ItemSearch> SearchQueries { get; set; } = new HashSet<ItemSearch>();
}
