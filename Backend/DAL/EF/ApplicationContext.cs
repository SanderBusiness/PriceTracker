using Domain.Items;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class ApplicationContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemPrice> Prices { get; set; }
    public DbSet<ItemSearch> Searches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(nameof(ApplicationContext));
        optionsBuilder.UseProjectables();
        base.OnConfiguring(optionsBuilder);
    }
}