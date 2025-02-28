import { useState } from "react";
import { signup } from "../api";
import { useNavigate } from "react-router-dom";
import { Button, TextField, Typography, Box } from "@mui/material";

const Signup = () => {
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const handleSignup = async () => {
        try {
            await signup(username, email, password);
            navigate("/login");
        } catch (error) {
            alert("Signup failed");
        }
    };

    return (
        <Box>
            <Typography variant="h4">Signup</Typography>
            <TextField label="Username" value={username} onChange={(e) => setUsername(e.target.value)} fullWidth margin="normal" />
            <TextField label="Email" value={email} onChange={(e) => setEmail(e.target.value)} fullWidth margin="normal" />
            <TextField label="Password" type="password" value={password} onChange={(e) => setPassword(e.target.value)} fullWidth margin="normal" />
            <Button variant="contained" color="primary" onClick={handleSignup}>Signup</Button>
        </Box>
    );
};

export default Signup;
