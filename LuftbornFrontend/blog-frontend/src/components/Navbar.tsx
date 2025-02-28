import { AppBar, Toolbar, Typography, Button, Box } from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import { useContext } from "react";
import { AuthContext } from "../AuthContext";

interface NavbarProps {
    user: any;
    setUser: (user: any) => void;
}

const Navbar = ({ user, setUser }: NavbarProps) => {
    const navigate = useNavigate();
    const auth = useContext(AuthContext);

    const handleLogout = () => {
        localStorage.removeItem("token");
        setUser(null);
        if (auth) {
            auth.logout();
        }
        navigate("/login");
    };

    return (
        <AppBar position="static">
            <Toolbar>
                <Typography variant="h6" sx={{ flexGrow: 1 }}>
                    <Link to="/posts" style={{ color: "inherit", textDecoration: "none" }}>
                        BlogApp
                    </Link>
                </Typography>
                <Box>
                    <Button color="inherit" component={Link} to="/posts">
                        All Posts
                    </Button>
                    {user ? (
                        <>
                            <Button color="inherit" component={Link} to="/my-posts">
                                My Posts
                            </Button>
                            <Button color="inherit" component={Link} to="/profile">
                                Profile
                            </Button>
                            <Button color="inherit" component={Link} to="/create-post">
                                Create Post
                            </Button>
                            <Button color="inherit" onClick={handleLogout}>
                                Logout
                            </Button>
                        </>
                    ) : (
                        <>
                            <Button color="inherit" component={Link} to="/login">
                                Login
                            </Button>
                            <Button color="inherit" component={Link} to="/signup">
                                Signup
                            </Button>
                        </>
                    )}
                </Box>
            </Toolbar>
        </AppBar>
    );
};

export default Navbar;