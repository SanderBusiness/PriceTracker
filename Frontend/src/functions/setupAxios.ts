import axios from "axios";

axios.defaults.baseURL = "http://localhost:5024"
if (!document.URL.includes("localhost"))
    axios.defaults.baseURL = "https://pricetracker-api.sanderc.net"
