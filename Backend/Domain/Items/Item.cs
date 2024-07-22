using Domain.Abstract;
using System.Text.Json.Serialization;
using EntityFrameworkCore.Projectables;
using Newtonsoft.Json.Converters;

namespace Domain.Items;

public class Item : Entity
{
    [JsonConverter(typeof(StringEnumConverter))]
    public required Shop Shop { get; set; }
    public required string Title { get; set; }
    public required string Url { get; set; }
    public required string Image { get; set; }
    [Projectable] public decimal LatestPrice => PriceHistory
        .OrderByDescending(e => e.CreatedOn)
        .First().Price;

    public ICollection<ItemPrice> PriceHistory { get; set; } = new HashSet<ItemPrice>();
    public ICollection<ItemSearch> SearchQueries { get; set; } = new HashSet<ItemSearch>();
}
