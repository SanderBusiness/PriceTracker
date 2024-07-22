import {PriceHistory} from "./PriceHistory";
import {SearchQuery} from "./SearchQuery";

export interface Product {
    shop: number
    title: string
    url: string
    image: string
    latestPrice:number
    priceHistory: PriceHistory[]
    searchQueries: SearchQuery[]
    id: string
    createdOn: string
}
