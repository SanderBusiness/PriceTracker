using Domain;
using Models;

namespace Scrapers.Abstract;

public interface IDiscoveryScraper
{
    protected Shop Shop { get; }
    public Task<List<DiscoveredItem>> Discover(string search);
}