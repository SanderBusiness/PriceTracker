using Domain;
using HtmlAgilityPack;
using Scrapers.Abstract;
using Scrapers.Helpers;

namespace Scrapers.Carrefour;

public class CarrefourDetailScraper : DetailsScraper
{
    public override Shop Shop => Shop.Carrefour;
    protected override async Task<string> FetchPage(string url)
    {
        var client = ScraperHttpClient.Get();
        //client.DefaultRequestHeaders.Add("Cookie", "ApplicationGatewayAffinityCORS=82344efa8b2a852143a5d382aeebc846; carrefour_be_lang=default; dw_store=0615; dwanonymous_3e7c08d0a46f12ebb778e9dc0c5c9f8e=abyPcajehLuSAvHMkHUbaYlKGt; __cq_dnt=1; dw_dnt=1; OptanonAlertBoxClosed=2024-07-06T17:02:27.906Z; __cf_bm=qfQTTOdYqlFccGTwdQ0bBRvhCDdSuvQTXQJpZO8oAUE-1720480170-1.0.1.1-MchRKtD7UjaVUzFiJPKFQfrRgxnCLXMk5VwVcv.Ah8SzVvYDaflp3.V3wy1yq9gQ4NTP20FGIYydlDliLd.ODg; sid=-Sk5R4C9mm-qDeJ1DdSZDL5GbQDCUZ01mO0; dwsid=HmGpna5ssQbjlylasSsrOFkO_drsgLZc0XdwglcWUG727GTTWpnXPJ8qG_FWfkp15hMuGILGr6_QSSFcP4KMdg==; cf_clearance=6zd9JMQU2GkbZRmsXRD02QYt4f0gjOZLX05EV9yrJjM-1720480173-1.0.1.1-s4HaL2Q8gi99nUtJseVvSmCptID.uwtIDaWk5nmWRLuDPRwNJka0GIICtmahpaS4RlbdxjEJCEmUTgQsfGxYcw; OptanonConsent=isGpcEnabled=0&datestamp=Tue+Jul+09+2024+01%3A10%3A06+GMT%2B0200+(Central+European+Summer+Time)&version=202403.1.0&browserGpcFlag=0&isIABGlobal=false&hosts=&consentId=8c7838a5-d426-489c-8459-0b35fc8e8389&interactionCount=1&isAnonUser=1&landingPath=NotLandingPage&groups=C0001%3A1%2CC0022%3A0%2CC0054%3A0%2CC0007%3A0%2CC0052%3A0%2CC0023%3A0%2CC0032%3A0%2CC0041%3A0%2CC0057%3A0%2CC0122%3A0%2CC0079%3A0%2CC0004%3A0&geolocation=%3B&AwaitingReconsent=false");
        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }

    protected override string GetTitle(HtmlDocument document)
    {
        var node = document.GetNodesWithClass("product-name").FirstOrDefault();
        return node == null 
            ? string.Empty 
            : node.InnerText;
    }

    protected override string GetDescription(HtmlDocument document)
    {
        var node = document.GetNodesWithClass("attribute-meer-details").FirstOrDefault();
        return node == null 
            ? string.Empty 
            : node.InnerText.Replace("\nMeer details\n", string.Empty);
    }

    protected override decimal GetPrice(HtmlDocument document)
    {
        var node = document.GetNodesWithClass("pricing-wrapper")
            .FirstOrDefault()?
            .GetSingleNodeWithClass("value");
        return node?.GetAttributeValue("content", (decimal) -1) ?? -1;
    }

    protected override string GetImage(HtmlDocument document)
    {
        var node = document.GetNodesWithClass("zoom-overlay__mainImage").FirstOrDefault();
        return node == null 
            ? string.Empty 
            : node.GetAttributeValue("src", string.Empty);
    }
}
