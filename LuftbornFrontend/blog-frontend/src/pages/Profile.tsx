import { useState, useEffect } from "react";
import { Typography, Box, Paper, Avatar, Divider, CircularProgress } from "@mui/material";
import { fetchUserProfile } from "../api";

interface UserProfile {
  id: string;
  username: string;
  email: string;
  createdAt: string;
}

const Profile = () => {
  const [profile, setProfile] = useState<UserProfile | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadProfile = async () => {
      try {
        setLoading(true);
        const data = await fetchUserProfile();
        setProfile(data);
        setLoading(false);
      } catch (error) {
        console.error("Failed to fetch profile:", error);
        setLoading(false);
      }
    };

    loadProfile();
  }, []);

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" mt={4}>
        <CircularProgress />
      </Box>
    );
  }

  if (!profile) {
    return (
      <Typography variant="h6" color="error" align="center" mt={4}>
        Failed to load profile. Please try again later.
      </Typography>
    );
  }

  return (
    <Paper elevation={3} sx={{ p: 3, mt: 3 }}>
      <Box display="flex" alignItems="center" mb={3}>
        <Avatar
          sx={{ width: 80, height: 80, bgcolor: "primary.main", mr: 3 }}
        >
          {profile.username.charAt(0).toUpperCase()}
        </Avatar>
        <Typography variant="h4">{profile.username}'s Profile</Typography>
      </Box>
      
      <Divider sx={{ mb: 3 }} />
      
      <Box mb={2}>
        <Typography variant="subtitle1" color="text.secondary">
          Email
        </Typography>
        <Typography variant="body1">{profile.email}</Typography>
      </Box>
      
      <Box mb={2}>
        <Typography variant="subtitle1" color="text.secondary">
          Username
        </Typography>
        <Typography variant="body1">{profile.username}</Typography>
      </Box>
      
      <Box>
        <Typography variant="subtitle1" color="text.secondary">
          Member Since
        </Typography>
        <Typography variant="body1">
          {new Date(profile.createdAt).toLocaleDateString()}
        </Typography>
      </Box>
    </Paper>
  );
};

export default Profile;