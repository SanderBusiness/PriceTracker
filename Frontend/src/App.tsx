import {Container} from "@mui/material";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Api from "./pages/Api";
import Search from "./pages/Search";
import "./functions/setupAxios"
import {SnackbarProvider} from "notistack";

function App() {
    return <Container maxWidth="md" sx={{maxHeight: "100vh"}}>
        <SnackbarProvider maxSnack={5}>
            <BrowserRouter>
                <Routes>
                    <Route path={"api"} element={<Api/>}/>
                    <Route path={":id"} element={<Search/>}/>
                    <Route index element={<Search/>}/>
                </Routes>
            </BrowserRouter>
        </SnackbarProvider>
    </Container>
}

export default App;
