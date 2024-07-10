using Domain;
using Newtonsoft.Json;
using Scrapers.Abstract;
using Scrapers.Helpers;

namespace Scrapers.Delhaize;

public class DelhaizeScraper : DiscoveryScraper<Product, DelhaizeModel>
{
    public override Shop Shop => Shop.Delhaize;
    
    protected override async Task<DelhaizeModel> FetchPage(string search)
    {
        var client = new HttpClient();
        client.AddUserHeaders();
        var response = await client.GetAsync(new Uri("https://www.delhaize.be/api/v1/?operationName=GetProductSearch&variables=%7B%22lang%22%3A%22nl%22%2C%22searchQuery%22%3A%22^" +
                                                     $"{search}" +
                                                     "%3Arelevance%22%2C%22sort%22%3A%22relevance%22%2C%22pageNumber%22%3A0%2C%22pageSize%22%3A20%2C%22" +
                                                     "filterFlag%22%3Atrue%2C%22plainChildCategories%22%3Atrue%2C%22useSpellingSuggestion%22%3Atrue%7D" +
                                                     "&extensions=%7B%22persistedQuery%22%3A%7B%22" +
                                                     "version%22%3A1%2C%22sha256Hash%22%3A%224217b5494fb56322781813a4aa20452e0147e02d45ffb6e0371f0fc6ab792308%22%7D%7D"));
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<DelhaizeModel>(responseBody) ?? new DelhaizeModel();
    }

    protected override IEnumerable<Product> GetItems(DelhaizeModel document)
    {
        return document?.data?.productSearch?.products ?? [];
    }

    protected override string GetTitle(Product node)
    {
        return node.name;
    }

    protected override string GetUrl(Product node)
    {
        return $"https://www.delhaize.be/{node.url}";
    }

    protected override decimal GetPrice(Product node)
    {
        return (decimal) node.price.value;
    }

    protected override string GetImage(Product node)
    {
        return $"https://www.delhaize.be/{node.images.First().url}";
    }
}