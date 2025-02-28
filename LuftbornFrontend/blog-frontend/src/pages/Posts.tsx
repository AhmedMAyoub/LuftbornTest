import { useEffect, useState, useContext } from "react";
import { fetchPosts, logout } from "../api";
import { AuthContext } from "../AuthContext";
import { Button, Typography, Box } from "@mui/material";
import { useNavigate } from "react-router-dom";

const Posts = () => {
  const [posts, setPosts] = useState([]);
  const auth = useContext(AuthContext);
  const navigate = useNavigate();

  useEffect(() => {
    const loadPosts = async () => {
      try {
        const data = await fetchPosts();
        setPosts(data);
      } catch (error) {
        alert("Failed to fetch posts");
      }
    };
    loadPosts();
  }, []);

  return (
    <Box>
      <Typography variant="h4">Blog Posts</Typography>
      {posts.map((post: any) => (
        <Box key={post.id} mt={2} p={2} border={1} borderRadius={2}>
          <Typography variant="h6">{post.title}</Typography>
          <Typography>{post.content}</Typography>
        </Box>
      ))}
    </Box>
  );
};

export default Posts;
