import {Product} from "../types/Product";
import {ImageListItem, ImageListItemBar} from "@mui/material";
import React from "react";
import {useNavigate} from "react-router-dom";

export default function ProductCard({product}: { product: Product }) {
    const navigate = useNavigate()

    return <>
        <ImageListItem key={product.image}>
            <img
                onClick={() => navigate(`/${product.id}`)}
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
