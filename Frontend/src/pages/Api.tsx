import "./../css/api.css"
import {Link} from "react-router-dom";

export default function Api() {
    return <>
        <Link to={"/"}>Home</Link>
        <iframe title={"Api reference"} src={"https://pricetracker-api.sanderc.net/swagger"}  />
    </>
}
