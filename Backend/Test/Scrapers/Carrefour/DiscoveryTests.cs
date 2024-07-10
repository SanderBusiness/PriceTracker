using FluentAssertions;
using Scrapers.Carrefour;
using Scrapers.Helpers;

namespace Test.Scrapers.Carrefour;

[TestFixture]
public class DiscoveryTests
{
    [Test]
    public async Task SomeResults()
    {
        var scraper = new CarrefourScraper();
        var items = await scraper.Discover("banana");
        items.Count.Should().BeGreaterThan(0);
    }
    [Test]
    public async Task NoResults()
    {
        var scraper = new CarrefourScraper();
        var items = await scraper.Discover("____");
        items.Count.Should().Be(0);
    }
}