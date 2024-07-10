// ReSharper disable All
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Scrapers.Delhaize;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Badge
    {
        public string code { get; set; }
        public Image image { get; set; }
        public string tooltipMessage { get; set; }
        public object name { get; set; }
        public string __typename { get; set; }
    }

    public class CurrentQuery
    {
        public Query query { get; set; }
        public string __typename { get; set; }
    }

    public class Data
    {
        public ProductSearch productSearch { get; set; }
    }

    public class Facet
    {
        public string code { get; set; }
        public string name { get; set; }
        public bool category { get; set; }
        public string facetUiType { get; set; }
        public List<Value> values { get; set; }
        public string __typename { get; set; }
    }

    public class Image
    {
        public object altText { get; set; }
        public object format { get; set; }
        public object galleryIndex { get; set; }
        public object imageType { get; set; }
        public string url { get; set; }
        public string __typename { get; set; }
    }

    public class Image2
    {
        public string format { get; set; }
        public string imageType { get; set; }
        public string url { get; set; }
        public string __typename { get; set; }
    }

    public class MobileFee
    {
        public string feeName { get; set; }
        public int feeValue { get; set; }
        public string __typename { get; set; }
    }

    public class Pagination
    {
        public int currentPage { get; set; }
        public int totalResults { get; set; }
        public int totalPages { get; set; }
        public string sort { get; set; }
        public string __typename { get; set; }
    }

    public class Price
    {
        public string approximatePriceSymbol { get; set; }
        public string currencySymbol { get; set; }
        public string formattedValue { get; set; }
        public string priceType { get; set; }
        public string supplementaryPriceLabel1 { get; set; }
        public string supplementaryPriceLabel2 { get; set; }
        public bool showStrikethroughPrice { get; set; }
        public string discountedPriceFormatted { get; set; }
        public string discountedUnitPriceFormatted { get; set; }
        public string unit { get; set; }
        public string unitPriceFormatted { get; set; }
        public string unitCode { get; set; }
        public double unitPrice { get; set; }
        public double value { get; set; }
        public string __typename { get; set; }
    }

    public class Product
    {
        public bool available { get; set; }
        public object averageRating { get; set; }
        public object numberOfReviews { get; set; }
        public string manufacturerName { get; set; }
        public string manufacturerSubBrandName { get; set; }
        public string code { get; set; }
        public object country { get; set; }
        public object countryFlagUrl { get; set; }
        public List<Badge> badges { get; set; }
        public object badgeBrand { get; set; }
        public object promoBadges { get; set; }
        public bool? delivered { get; set; }
        public bool? littleLion { get; set; }
        public int? freshnessDuration { get; set; }
        public string freshnessDurationTipFormatted { get; set; }
        public bool? frozen { get; set; }
        public bool recyclable { get; set; }
        public List<Image> images { get; set; }
        public bool isBundle { get; set; }
        public object isProductWithOnlineExclusivePromo { get; set; }
        public object isWine { get; set; }
        public int maxOrderQuantity { get; set; }
        public object limitedAssortment { get; set; }
        public List<MobileFee> mobileFees { get; set; }
        public string name { get; set; }
        public object newProduct { get; set; }
        public bool? onlineExclusive { get; set; }
        public List<object> potentialPromotions { get; set; }
        public Price price { get; set; }
        public object purchasable { get; set; }
        public object productPackagingQuantity { get; set; }
        public int productProposedPackaging { get; set; }
        public object productProposedPackaging2 { get; set; }
        public Stock stock { get; set; }
        public string url { get; set; }
        public object previouslyBought { get; set; }
        public string nutriScoreLetter { get; set; }
        public object isLowPriceGuarantee { get; set; }
        public object isHouseholdBasket { get; set; }
        public object isPermanentPriceReduction { get; set; }
        public object freeGift { get; set; }
        public object plasticFee { get; set; }
        public string __typename { get; set; }
    }

    public class ProductsCountByCategory
    {
        public string categoryCode { get; set; }
        public int count { get; set; }
        public string __typename { get; set; }
    }

    public class ProductSearch
    {
        public List<Product> products { get; set; }
        public List<object> breadcrumbs { get; set; }
        public List<Facet> facets { get; set; }
        public List<Sort> sorts { get; set; }
        public Pagination pagination { get; set; }
        public string freeTextSearch { get; set; }
        public bool spellingSuggestionUsed { get; set; }
        public CurrentQuery currentQuery { get; set; }
        public List<ProductsCountByCategory> productsCountByCategory { get; set; }
        public string __typename { get; set; }
    }

    public class Query
    {
        public Query query { get; set; }
        public string __typename { get; set; }
        public string value { get; set; }
    }

    public class DelhaizeModel
    {
        public Data data { get; set; }
    }

    public class Sort
    {
        public string name { get; set; }
        public bool selected { get; set; }
        public string code { get; set; }
        public string __typename { get; set; }
    }

    public class Stock
    {
        public bool inStock { get; set; }
        public bool inStockBeforeMaxAdvanceOrderingDate { get; set; }
        public bool partiallyInStock { get; set; }
        public object availableFromDate { get; set; }
        public string __typename { get; set; }
    }

    public class Value
    {
        public string code { get; set; }
        public int count { get; set; }
        public string name { get; set; }
        public Query query { get; set; }
        public bool selected { get; set; }
        public string __typename { get; set; }
    }

