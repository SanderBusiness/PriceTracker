using System.ComponentModel.DataAnnotations;
using Domain.Abstract;

namespace Domain.Items;

public class ItemSearch : Entity
{
    [MaxLength(50)] public required string Search { get; set; }
    public required Item Item { get; set; }
}