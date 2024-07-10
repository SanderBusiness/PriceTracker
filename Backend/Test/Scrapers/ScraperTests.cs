using FluentAssertions;
using Scrapers.Abstract;
using Scrapers.Carrefour;

namespace Test.Scrapers;

[TestFixture]
public class ScraperTests
{
    [TestCase(typeof(CarrefourScraper))]
    //[TestCase(typeof(DelhaizeScraper))]
    public async Task SomeResults(Type scraperType)
    {
        var scraper = Activator.CreateInstance(scraperType) as IDiscoveryScraper;
        var items = await scraper!.Discover("banana");
        items.Count.Should().BeGreaterThan(0);
    }
    
    [TestCase(typeof(CarrefourScraper))]
    //[TestCase(typeof(DelhaizeScraper))]
    public async Task NoResults(Type scraperType)
    {
        var scraper = Activator.CreateInstance(scraperType) as IDiscoveryScraper;
        var items = await scraper!.Discover("____");
        items.Count.Should().Be(0);
    }
}