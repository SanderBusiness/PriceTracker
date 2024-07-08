using Scrapers.Abstract;
using Scrapers.Carrefour;

namespace BL.Helpers;

public static class ScraperFactory
{
    public static List<DiscoveryScraper> DiscoveryScrapers()
        => [new CarrefourDiscoveryScraper()];
    
    public static List<DetailsScraper> DetailScrapers()
        => [new CarrefourDetailScraper()];
}