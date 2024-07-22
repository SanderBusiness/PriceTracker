import {Product} from "../types/Product";
import {ImageListItem, ImageListItemBar} from "@mui/material";
import React from "react";

export default function ProductCard({product}: { product: Product }) {
    return <>
        <ImageListItem key={product.image}>
            <img
                srcSet={product.image}
                src={product.image}
                alt={product.title}
                style={{cursor: "pointer"}}
                loading="lazy"
            />
            <ImageListItemBar position="bottom" title={`€ ${product.latestPrice} | ${product.title}`}/>
        </ImageListItem>
    </>
}
