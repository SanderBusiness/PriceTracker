using System.Net;
using System.Net.Http;

namespace Scrapers.Helpers;

public static class ScraperHttpClient
{
    private static HttpClient _client = null;
    private static DateTimeOffset _lastRefresh = DateTimeOffset.MinValue;
    
    public static HttpClient Get()
    {
        if (_client != null && _lastRefresh > DateTimeOffset.Now.AddMinutes(-2)) return _client;
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = true,
            UseCookies = true,
            CookieContainer = new CookieContainer()
        };
        _client = new HttpClient(handler);
        _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36 Edg/126.0.0.0");
        _client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
        _client.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.9,en-US;q=0.8,nl;q=0.7");
        _client.DefaultRequestHeaders.Add("Sec-Ch-Ua-Platform", "\"Windows\"");
        _client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
        _client.DefaultRequestHeaders.Add("Sec-Ch-Ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\", \"Microsoft Edge\";v=\"126\"");
        _client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
        _client.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
        _lastRefresh = DateTimeOffset.Now;
        return _client;
    }
}
