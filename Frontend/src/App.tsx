import {Container} from "@mui/material";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Api from "./pages/Api";
import Product from "./pages/Product";
import Search from "./pages/Search";
import NavigationBar from "./components/system/NavigationBar";

function App() {
  return <Container maxWidth="md" sx={{maxHeight: "100vh"}}>
      <BrowserRouter>
          <NavigationBar />
          <Routes>
              <Route path={"api"} element={<Api />} />
              <Route path={"product"} element={<Product />} />
              <Route index={true} element={<Search />} />
          </Routes>
      </BrowserRouter>
  </Container>
}

export default App;
