import useSearch from "../../hooks/useSearch";
import {ImageList, Typography} from "@mui/material";
import ProductCard from "../ProductCard";

export function SearchResults() {
    const {products, loading, query} = useSearch()
    return <>
        {query.length > 0 && !loading && <Typography variant={"subtitle2"} sx={{mb: 3}}>
            We hebben {products.length} resultaten gevonden voor: {query}
        </Typography>}
        <ImageList variant="masonry" cols={5} gap={8}>
            {products.map(product => <ProductCard product={product} key={product.id}/>)}
        </ImageList>
    </>
}
