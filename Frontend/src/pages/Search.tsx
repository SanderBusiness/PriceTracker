import useSearch from "../hooks/useSearch";
import {ImageList, LinearProgress, Typography} from "@mui/material";
import ProductCard from "../components/ProductCard";

export default function Search() {
    const {products, loading, query} = useSearch()

    return <>
        {loading && <LinearProgress/>}
        {query.length > 0 && !loading && <Typography variant={"subtitle2"} sx={{mb: 3}}>
            We hebben {products.length} resultaten gevonden voor: {query}
        </Typography>}
        <ImageList variant="masonry" cols={5} gap={8}>
            {products.map(product => <ProductCard product={product} key={product.id}/>)}
        </ImageList>
    </>
}
