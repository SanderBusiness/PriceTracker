import {AppBar, Box, Button} from "@mui/material";
import {useNavigate} from "react-router-dom";

export default function NavigationBar() {
    const navigate = useNavigate()
    return <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' }, mx:2 }}>
            <Button
                onClick={() => navigate("/")}
                sx={{ my: 2, display: 'block' }}
            >
                Home
            </Button>
        </Box>
}
