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
    }
    
    [Test]
    public async Task Chips()
    {
        var scraper = new CarrefourDetailScraper();
        var item = await scraper.Scrape("https://www.carrefour.be/nl/original-hartige-snack-165-g/07090377.html");
        item.Price.Should().BeGreaterThan(0);
        item.Details.Title.Should().NotBeEmpty();
        item.Details.Image.Should().NotBeEmpty();
    }
}