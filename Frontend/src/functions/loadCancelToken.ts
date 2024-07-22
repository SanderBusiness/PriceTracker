import axios from "axios";

export default function loadCancelToken() {
    const CancelToken = axios.CancelToken
    return CancelToken.source()
}
