import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, TextField, Typography, Box, Paper } from "@mui/material";
import { createPost } from "../api";

const CreatePost = () => {
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!title || !content) {
      alert("Please fill in all fields");
      return;
    }

    setLoading(true);
    try {
      await createPost(title, content);
      setLoading(false);
      navigate("/posts");
    } catch (error) {
      setLoading(false);
      alert("Failed to create post");
      console.error(error);
    }
  };

  return (
    <Paper elevation={3} sx={{ p: 3, mt: 3 }}>
      <Typography variant="h4" gutterBottom>
        Create New Post
      </Typography>
      <Box component="form" onSubmit={handleSubmit}>
        <TextField
          label="Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          fullWidth
          margin="normal"
          required
        />
        <TextField
          label="Content"
          value={content}
          onChange={(e) => setContent(e.target.value)}
          fullWidth
          margin="normal"
          multiline
          rows={6}
          required
        />
        <Button
          type="submit"
          variant="contained"
          color="primary"
          disabled={loading}
          sx={{ mt: 2 }}
        >
          {loading ? "Creating..." : "Create Post"}
        </Button>
        <Button
          variant="outlined"
          onClick={() => navigate("/posts")}
          sx={{ mt: 2, ml: 2 }}
        >
          Cancel
        </Button>
      </Box>
    </Paper>
  );
};

export default CreatePost;