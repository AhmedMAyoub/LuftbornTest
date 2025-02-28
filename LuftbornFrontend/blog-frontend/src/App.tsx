import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { useState, useEffect, useContext } from "react";
import Signup from "./pages/Signup";
import Login from "./pages/Login";
import Posts from "./pages/Posts";
import CreatePost from "./pages/CreatePost";
import Profile from "./pages/Profile";
import MyPosts from "./pages/MyPosts";
import Navbar from "./components/Navbar";
import { Container, CssBaseline } from "@mui/material";
import { AuthProvider, AuthContext } from "./AuthContext";
import { getUserData } from "./api";

function AppContent() {
  const [user, setUser] = useState(null);
  const auth = useContext(AuthContext);

  useEffect(() => {
    const fetchUserData = async () => {
      const userData = await getUserData();
      if (userData) {
        setUser(userData);
      } else {
        setUser(null);
        if (auth) auth.logout();
      }
    };

    if (auth?.token) {
      fetchUserData();
    } else {
      setUser(null);
    }
  }, [auth?.token]);

  return (
    <Router>
      <CssBaseline />
      <Navbar user={user} setUser={setUser} />
      <Container>
        <Routes>
          <Route path="/signup" element={<Signup />} />
          <Route path="/login" element={<Login />} />
          <Route path="/posts" element={<Posts />} />
          <Route path="/profile" element={user ? <Profile /> : <Login />} />
          <Route path="/create-post" element={user ? <CreatePost /> : <Login />} />
          <Route path="/my-posts" element={user ? <MyPosts /> : <Login />} />
        </Routes>
      </Container>
    </Router>
  );
}

function App() {
  return (
    <AuthProvider>
      <AppContent />
    </AuthProvider>
  );
}

export default App;