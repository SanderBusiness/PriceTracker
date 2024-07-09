using Domain;
using Domain.Items;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Models;

namespace Scrapers.Abstract;

public abstract class DetailsScraper(ILoggerFactory loggerFactory)
{
    protected abstract Shop Shop { get; }
    protected abstract Task<string> FetchPage(string url);
    protected abstract string GetTitle(HtmlDocument document);
    protected abstract string GetDescription(HtmlDocument document);
    protected abstract decimal GetPrice(HtmlDocument document);
    protected abstract string GetImage(HtmlDocument document);
    
    public async Task<ScrapedItem?> Scrape(string url)
    {
        var page = await FetchPage(url);
        if (string.IsNullOrEmpty(page))
        {
            loggerFactory.CreateLogger<DetailsScraper>().LogInformation("No page found on: {Url}", url);
            return null;
        }
        var document = new HtmlDocument();
        document.LoadHtml(page);

        return new ScrapedItem()
        {
            Details = new Item
            {
                Shop = Shop,
                Url = url,
                Title = GetTitle(document),
                Description = GetDescription(document),
                Image = GetImage(document),
            },
            Price = GetPrice(document)
        };
    }
}