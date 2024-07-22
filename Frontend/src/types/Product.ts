import {PriceHistory} from "./PriceHistory";
import {SearchQuery} from "./SearchQuery";
import {SuperMarket} from "./SuperMarket";

export interface Product {
    shop: SuperMarket
    title: string
    url: string
    image: string
    latestPrice: number
    priceHistory: PriceHistory[]
    searchQueries: SearchQuery[]
    id: string
    createdOn: string
}
