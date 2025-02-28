import { useState, useContext } from "react";
import { login } from "../api";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../AuthContext";
import { Button, TextField, Typography, Box } from "@mui/material";

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();
  const auth = useContext(AuthContext);

  const handleLogin = async () => {
    try {
      const data = await login(username, password);
      auth?.login(data.token);
      navigate("/posts");
    } catch (error) {
      alert("Login failed");
    }
  };

  return (
    <Box>
      <Typography variant="h4">Login</Typography>
      <TextField label="Username" value={username} onChange={(e) => setUsername(e.target.value)} fullWidth margin="normal" />
      <TextField label="Password" type="password" value={password} onChange={(e) => setPassword(e.target.value)} fullWidth margin="normal" />
      <Button variant="contained" color="primary" onClick={handleLogin}>Login</Button>
    </Box>
  );
};

export default Login;
