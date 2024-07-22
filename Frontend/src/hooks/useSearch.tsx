import React, {createContext, useContext, useEffect, useState} from "react";
import {Product} from "../types/Product";
import {TextField} from "@mui/material";
import {useDebounce} from "use-debounce";
import axios from "axios";
import loadCancelToken from "../functions/loadCancelToken";

interface SearchContextProps {
    setQuery: (query: string) => void,
    query: string,
    loading: boolean,
    products: Product[],
}

const SearchContext = createContext<SearchContextProps>({
    query: '',
    setQuery: () => {},
    loading: false,
    products: []
})

let source = loadCancelToken()
const cancelMsg = "Filter value has changed"
export function SearchProvider({ children }: { children: React.ReactNode }) {
    const [filter, setFilter] = useState("");
    const [typing, setTyping] = useState(false)
    const [value] = useDebounce(filter, 500);
    const [loading, setLoading] = useState(false);
    const [products, setProducts] = useState<Product[]>([])

    useEffect(() => {
        setTyping(false)
    }, [value])

    function setQuery(value: string) {
        if (loading) {
            source.cancel(cancelMsg)
            source = loadCancelToken()
            setLoading(false)
        }
        if (!typing) setTyping(true)
        setFilter(value)
    }
    useEffect(() => {
        try {
            console.log("ran")
            if (isValidFilter(value)) {
                console.log("go go")
                setLoading(true)
                setProducts([])
                axios
                    .get<Product[]>(`/Item/Search?s=${value}`, {timeout: 0, cancelToken: source.token})
                    .then(res => {
                        setProducts(res.data)
                        setLoading(false)
                    })
                    .catch(err => {
                        if (`${err.name}` !== "CanceledError") console.log(err.message, {variant: "error"})
                    })
            } else {
                setProducts([])
                if (value.length > 0 && value.length < 3)
                    console.log("Je moet minstens 3 karakters invullen voordat we beginnen te zoeken!", {variant: "info"})
            }
        } catch (e: any) {
            console.log("Error: ", e)
            setLoading(false)
        }
    }, [value])

    return <SearchContext.Provider value={{query: value, setQuery:setQuery, loading, products}}>
        <TextField sx={{width: "100%"}} type="text" placeholder="Zoek product..." onChange={e => setQuery(e.target.value)} />
        {children}
    </SearchContext.Provider>
}

export default function useSearch() {
    return useContext(SearchContext);
}

function isValidFilter(value:string) {
    return value.length > 3
}
