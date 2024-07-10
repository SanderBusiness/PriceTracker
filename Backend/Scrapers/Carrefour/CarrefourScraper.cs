using Domain;
using HtmlAgilityPack;
using Scrapers.Abstract;
using Scrapers.Helpers;

namespace Scrapers.Carrefour;

public class CarrefourScraper : DiscoveryScraper
{
    protected override Shop Shop => Shop.Carrefour;
    protected override async Task<string> FetchPage(string search)
    {
        var client = new HttpClient();
        client.AddUserHeaders();
        //client.DefaultRequestHeaders.Add("Cookie", "dw_store=0615; dwanonymous_3e7c08d0a46f12ebb778e9dc0c5c9f8e=bdJQlEab4NfghzKHzhpeSKVym2; sid=ZmDq5so7LGeQZjQ7tbojMJsXHuhIAOmpGe0; dwsid=JgXEDWarK7QfB186gYtY6dtyMAPkkO56lozOUUngUJQK0j3IgBoiEZgADff-i2VZZxdbrZxHUjE0MKu6vfxf5Q==; carrefour_be_lang=default; OptanonAlertBoxClosed=2024-07-10T11:28:01.611Z; _gcl_au=1.1.1939215718.1720610882; _gid=GA1.2.15875895.1720610882; _cs_c=0; _pin_unauth=dWlkPU16ZG1OMll6Wm1NdFkyTTVNUzAwT0dNMExUbG1OekF0T1RBeVpqazFZelUzTURObA; __cq_dnt=1; dw_dnt=1; __cf_bm=iZe.fewpVzERitzFKPnTEV6yoTD9eTKWEoz97213CRY-1720613550-1.0.1.1-l5Z0SILeKh.8GdRkNQQAi2S1RiULmLB2.KfJ6oK.TiIjQk.CijQM0MQQ8Rxbqucvj9ILKBwL9b7R6Sd9LauZEA; _cs_mk_ga=0.5722511361871692_1720613634296; ABTastySession=mrasn=&lp=https%253A%252F%252Fwww.carrefour.be%252Fnl%252Fsearch%253Fq%253DBananen; cf_clearance=_5CGA4atiDY9X.OEoMiABR2Zxo74qyumhMKPrEBSnng-1720613557-1.0.1.1-oKATLv7r5BfkzY54tDKqJ09w.3qx7NzkbdRZbxGbA9EnTdIMKN5iB_DbRhZUEBLS4NTWotbOO9hqY7TaFYJZZA; OptanonConsent=isGpcEnabled=0&datestamp=Wed+Jul+10+2024+14%3A24%3A26+GMT%2B0200+(Central+European+Summer+Time)&version=202403.1.0&browserGpcFlag=0&isIABGlobal=false&hosts=&consentId=18f10cb1-f042-4b1d-b3ae-06596b88126d&interactionCount=1&isAnonUser=1&landingPath=NotLandingPage&groups=C0001%3A1%2CC0022%3A1%2CC0054%3A1%2CC0007%3A1%2CC0052%3A1%2CC0023%3A1%2CC0032%3A1%2CC0041%3A1%2CC0057%3A1%2CC0122%3A1%2CC0079%3A1%2CC0004%3A1&geolocation=%3B&AwaitingReconsent=false; _ga_HZ1NJYS59D=GS1.1.1720610881.1.1.1720614267.0.0.1409138478; _ga=GA1.2.1756612208.1720610882; ABTasty=uid=gctqv3sn0t7nwreb&fst=1720610884021&pst=1720610884021&cst=1720613634853&ns=2&pvt=7&pvis=3&th=; _gat_UA-10408282-5=1; FPGSID=1.1720612772.1720614184.G-HZ1NJYS59D.crQWUL42OvsEhBOseJFgSg; _cs_id=54a705f5-3077-a5a5-d93a-d146a7f41910.1720610882.2.1720614268.1720613640.1.1754774882353.1; _cs_s=2.0.0.1720616068200");
        var response = await client.GetAsync($"https://www.carrefour.be/nl/search?q={search}&page=0");
        return await response.Content.ReadAsStringAsync();
    }

    protected override IEnumerable<HtmlNode> GetItems(HtmlDocument document)
    {
        return document.GetNodesWithClass("js-product");
    }

    protected override string GetTitle(HtmlNode node)
    {
        return node.GetSingleNodeWithClass("desktop-name").InnerText;
    }

    protected override string GetUrl(HtmlNode node)
    {
        node =  node.GetSingleNodeWithClass("js-product-tile-gtm");
        node = node.GetSingleNodeWithClass("image-container");
        var page = node.ChildNodes[1].GetAttributeValue("href", string.Empty);
        return page == null 
            ? string.Empty 
            : $"https://www.carrefour.be/nl/{page}";
    }

    protected override decimal GetPrice(HtmlNode node)
    {
        node =  node.GetSingleNodeWithClass("pricing-wrapper");
        var prices = node.Descendants()
            .Where(e => e.HasClass("value"))
            .Select(e => e.GetAttributeValue("content", "-1"))
            .Where(e => e != "-1")
            //.Select(e => e.Replace(".", ","))
            .Select(decimal.Parse)
            .ToList();
        if (!prices.Any())
            return -1;
        return prices.MinBy(e => e);
    }

    protected override string GetImage(HtmlNode node)
    {
        node =  node.GetSingleNodeWithClass("tile-image");
        return node.GetAttributeValue("src", string.Empty);
    }
}
