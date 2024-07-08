using FluentAssertions;
using Scrapers.Carrefour;

namespace Test.Scrapers.Carrefour;

[TestFixture]
public class DiscoveryTests
{
    [Test]
    public async Task SomeResults()
    {
        var scraper = new CarrefourDiscoveryScraper();
        var items = await scraper.Discover("banana");
        items.Count.Should().BeGreaterThan(0);
    }
    [Test]
    public async Task NoResults()
    {
        var scraper = new CarrefourDiscoveryScraper();
        var items = await scraper.Discover("____");
        items.Count.Should().Be(0);
    }
}