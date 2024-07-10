using BL.Implementations;
using BL.Interfaces;
using DAL.EF;
using Scrapers.Carrefour;

namespace API.Helpers.Program;

public static class Dependencies
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        return services
            .AddDbContext<ApplicationContext>()
            .AddScoped<IItemService, ItemService>()
            .AddTransient<IScraperService, ScraperService>()
            .AddSingleton<CarrefourScraper>();
    }
}
