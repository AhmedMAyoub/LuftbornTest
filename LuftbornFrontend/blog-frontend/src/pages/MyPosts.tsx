import { useState, useEffect } from "react";
import { 
  Typography, Box, Paper, Button, TextField, Dialog, 
  DialogActions, DialogContent, DialogTitle, CircularProgress,
  IconButton, Divider, Alert
} from "@mui/material";
import { fetchUserPosts, updatePost, deletePost } from "../api";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";

interface Post {
  id: string;
  title: string;
  content: string;
  createdAt: string;
}

const MyPosts = () => {
  const [posts, setPosts] = useState<Post[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  
  // Edit dialog state
  const [editDialogOpen, setEditDialogOpen] = useState(false);
  const [editingPost, setEditingPost] = useState<Post | null>(null);
  const [editTitle, setEditTitle] = useState("");
  const [editContent, setEditContent] = useState("");
  const [editLoading, setEditLoading] = useState(false);
  
  // Delete dialog state
  const [deleteDialogOpen, setDeleteDialogOpen] = useState(false);
  const [deletingPostId, setDeletingPostId] = useState<string | null>(null);
  const [deleteLoading, setDeleteLoading] = useState(false);

  const loadPosts = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await fetchUserPosts();
      setPosts(data);
    } catch (error) {
      setError("Failed to load your posts. Please try again.");
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadPosts();
  }, []);

  const handleEditClick = (post: Post) => {
    setEditingPost(post);
    setEditTitle(post.title);
    setEditContent(post.content);
    setEditDialogOpen(true);
  };

  const handleEditSubmit = async () => {
    if (!editingPost) return;
    
    try {
      setEditLoading(true);
      await updatePost(editingPost.id, editTitle, editContent);
      
      // Update local state
      setPosts(posts.map(post => 
        post.id === editingPost.id 
          ? { ...post, title: editTitle, content: editContent } 
          : post
      ));
      
      setEditDialogOpen(false);
    } catch (error) {
      console.error("Failed to update post:", error);
      setError("Failed to update post. Please try again.");
    } finally {
      setEditLoading(false);
    }
  };

  const handleDeleteClick = (postId: string) => {
    setDeletingPostId(postId);
    setDeleteDialogOpen(true);
  };

  const handleDeleteConfirm = async () => {
    if (!deletingPostId) return;
    
    try {
      setDeleteLoading(true);
      await deletePost(deletingPostId);
      
      // Update local state
      setPosts(posts.filter(post => post.id !== deletingPostId));
      setDeleteDialogOpen(false);
    } catch (error) {
      console.error("Failed to delete post:", error);
      setError("Failed to delete post. Please try again.");
    } finally {
      setDeleteLoading(false);
    }
  };

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" mt={4}>
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Box sx={{ py: 3 }}>
      <Typography variant="h4" gutterBottom>My Posts</Typography>
      
      {error && (
        <Alert severity="error" sx={{ mb: 2 }} onClose={() => setError(null)}>
          {error}
        </Alert>
      )}
      
      {posts.length === 0 ? (
        <Paper elevation={2} sx={{ p: 3, textAlign: "center" }}>
          <Typography variant="h6">You haven't created any posts yet</Typography>
          <Button 
            variant="contained" 
            color="primary" 
            sx={{ mt: 2 }} 
            href="/create-post"
          >
            Create Your First Post
          </Button>
        </Paper>
      ) : (
        posts.map((post) => (
          <Paper key={post.id} elevation={2} sx={{ mt: 2, p: 3, borderRadius: 2 }}>
            <Box display="flex" justifyContent="space-between" alignItems="center">
              <Typography variant="h5">{post.title}</Typography>
              <Box>
                <IconButton 
                  color="primary" 
                  onClick={() => handleEditClick(post)}
                  aria-label="edit post"
                >
                  <EditIcon />
                </IconButton>
                <IconButton 
                  color="error" 
                  onClick={() => handleDeleteClick(post.id)}
                  aria-label="delete post"
                >
                  <DeleteIcon />
                </IconButton>
              </Box>
            </Box>
            
            <Divider sx={{ my: 1 }} />
            
            <Typography variant="body1" sx={{ mt: 1 }}>
              {post.content}
            </Typography>
            
            <Typography variant="caption" color="text.secondary" sx={{ mt: 2, display: "block" }}>
              Posted on: {new Date(post.createdAt).toLocaleString()}
            </Typography>
          </Paper>
        ))
      )}

      {/* Edit Dialog */}
      <Dialog 
        open={editDialogOpen} 
        onClose={() => !editLoading && setEditDialogOpen(false)}
        fullWidth
        maxWidth="md"
      >
        <DialogTitle>Edit Post</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            label="Title"
            fullWidth
            value={editTitle}
            onChange={(e) => setEditTitle(e.target.value)}
            disabled={editLoading}
          />
          <TextField
            margin="dense"
            label="Content"
            fullWidth
            multiline
            rows={6}
            value={editContent}
            onChange={(e) => setEditContent(e.target.value)}
            disabled={editLoading}
          />
        </DialogContent>
        <DialogActions>
          <Button 
            onClick={() => setEditDialogOpen(false)} 
            disabled={editLoading}
          >
            Cancel
          </Button>
          <Button 
            onClick={handleEditSubmit} 
            variant="contained" 
            color="primary"
            disabled={editLoading || !editTitle || !editContent}
          >
            {editLoading ? "Saving..." : "Save Changes"}
          </Button>
        </DialogActions>
      </Dialog>

      {/* Delete Confirmation Dialog */}
      <Dialog
        open={deleteDialogOpen}
        onClose={() => !deleteLoading && setDeleteDialogOpen(false)}
      >
        <DialogTitle>Confirm Delete</DialogTitle>
        <DialogContent>
          <Typography>
            Are you sure you want to delete this post? This action cannot be undone.
          </Typography>
        </DialogContent>
        <DialogActions>
          <Button 
            onClick={() => setDeleteDialogOpen(false)} 
            disabled={deleteLoading}
          >
            Cancel
          </Button>
          <Button 
            onClick={handleDeleteConfirm} 
            color="error" 
            variant="contained"
            disabled={deleteLoading}
          >
            {deleteLoading ? "Deleting..." : "Delete"}
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
};

export default MyPosts;