using FluentAssertions;
using Scrapers.Carrefour;

namespace Test.Scrapers.Carrefour;

[TestFixture]
public class DetailTests
{
    [Test]
    public async Task Water()
    {
        var scraper = new CarrefourDetailScraper();
        var item = await scraper.Scrape("https://www.carrefour.be/nl/cristal-roc-bronwater-5-l/04341568.html");
        item.Price.Should().BeGreaterThan(0);
        item.Details.Title.Should().NotBeEmpty();
        item.Details.Image.Should().NotBeEmpty();
        item.Details.Description.Should().NotBeEmpty();
    }
}