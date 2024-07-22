import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {Product} from "../../types/Product";
import axios from "axios";
import {Box, Skeleton, Typography} from "@mui/material";
import {useSnackbar} from "notistack";
import {SparkLineChart} from "@mui/x-charts";
import {SuperMarket} from "../../types/SuperMarket";

export default function SelectedResult() {
    const {id} = useParams()
    const [loading, setLoading] = useState<boolean>(false)
    const [product, setProduct] = useState<Product | null>(null)
    const {enqueueSnackbar} = useSnackbar()

    useEffect(() => {
        setLoading(true)
        if (id !== undefined) {
            axios.get<Product>(`/Item/Get?id=${id}`)
                .then(res => {
                    setProduct(res.data)
                })
                .catch(e => enqueueSnackbar(e, {variant: "error"}))
                .finally(() => setLoading(false))
        } else setProduct(null)
        setLoading(false)
    }, [id])

    if (loading)
        return <Skeleton sx={{width: '100%', height: '500px'}}/>

    if (product === null)
        return <></>

    const orderedPrices = product.priceHistory
        .sort((a, b) => new Date(a.createdOn).getTime() - new Date(b.createdOn).getTime())
        .splice(-30)

    return <Box sx={{mt: 2, mb: 6}}>
        <Typography variant={"h4"}><b>€ {product.latestPrice}</b> {SuperMarket[product.shop]} {product.title}
        </Typography>
        <img style={{maxWidth: "100%"}} src={product.image} alt={product.title}/>
        <SparkLineChart
            plotType="bar"
            data={orderedPrices.map(e => e.price)}
            height={100}
            showTooltip
            showHighlight
            xAxis={{
                data: orderedPrices.map(e => `€ ${e.price} | ${e.createdOn}`),
            }}
        />
    </Box>
}
