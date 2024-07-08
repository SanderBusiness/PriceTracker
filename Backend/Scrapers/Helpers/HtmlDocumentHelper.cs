using HtmlAgilityPack;

namespace Scrapers.Helpers;

public static class HtmlDocumentHelper
{
    public static IEnumerable<HtmlNode> GetNodesWithClass(this HtmlDocument document, string className)
        => document
            .DocumentNode
            .Descendants()
            .Where(e => e.HasClass(className))
            .ToList();
    
    public static HtmlNode GetSingleNodeWithClass(this HtmlNode node, string className)
        => node
            .Descendants()
            .First(e => e.HasClass(className));
}