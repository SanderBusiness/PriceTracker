import {SearchResults} from "../components/search/SearchResults";
import SelectedResult from "../components/search/SelectedResult";
import {Link} from "react-router-dom";
import useSearch, {SearchProvider} from "../hooks/useSearch";

export default function Search() {
    const {query} = useSearch()

    return <SearchProvider>
        <SelectedResult/>
        <SearchResults/>
        {query.length > 0 && <Link to={"/Api"}>Api</Link>}
    </SearchProvider>
}
