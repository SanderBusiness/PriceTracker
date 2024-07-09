using Microsoft.Extensions.Logging;
using Scrapers.Abstract;
using Scrapers.Helpers;

namespace Scrapers.Carrefour;

public class CarrefourScraper : Scraper
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly HttpClient _client;

    public CarrefourScraper(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        _client = new HttpClient();
        _client.AddUserHeaders();
    }

    protected override DiscoveryScraper DiscoveryScraper => new CarrefourDiscoveryScraper(_client);
    protected override DetailsScraper DetailsScraper => new CarrefourDetailScraper(_client, _loggerFactory);
}