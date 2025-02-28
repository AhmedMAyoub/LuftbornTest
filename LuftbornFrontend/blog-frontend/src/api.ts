const API_URL = "http://localhost:5151/api"; // Update with your backend URL

export const signup = async (username: string, email: string, password: string) => {
  const response = await fetch(`${API_URL}/users`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ username, email, password }),
  });

  if (!response.ok) throw new Error("Signup failed");

  return response.json();
};

export const login = async (username: string, password: string) => {
  const response = await fetch(`${API_URL}/auth/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ username, password }),
  });

  if (!response.ok) throw new Error("Invalid credentials");

  const data = await response.json();
  localStorage.setItem("token", data.token); // Store the JWT token
  return data;
};

export const fetchPosts = async () => {
  const token = localStorage.getItem("token");
  
  const response = await fetch(`${API_URL}/posts`, {
    headers: token ? { Authorization: `Bearer ${token}` } : {},
  });

  if (!response.ok) throw new Error("Failed to fetch posts");

  return response.json();
};

export const createPost = async (title: string, content: string) => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("Unauthorized");

  const response = await fetch(`${API_URL}/posts`, {
    method: "POST",
    headers: { 
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}` 
    },
    body: JSON.stringify({ title, content }),
  });

  if (!response.ok) throw new Error("Failed to create post");

  return response.json();
};

export const fetchUserProfile = async () => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("Unauthorized");

  const response = await fetch(`${API_URL}/users/me`, {
    headers: { Authorization: `Bearer ${token}` },
  });

  if (!response.ok) throw new Error("Failed to fetch user profile");

  return response.json();
};

export const getUserData = async () => {
  const token = localStorage.getItem("token");
  if (!token) return null;

  try {
    const response = await fetch(`${API_URL}/users/me`, {
      headers: { Authorization: `Bearer ${token}` },
    });

    if (!response.ok) {
      localStorage.removeItem("token");
      return null;
    }

    return response.json();
  } catch (error) {
    localStorage.removeItem("token");
    return null;
  }
};

export const logout = () => {
  localStorage.removeItem("token");
};

export const fetchUserPosts = async () => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("Unauthorized");

  const response = await fetch(`${API_URL}/posts/my-posts`, {
    headers: { Authorization: `Bearer ${token}` },
  });

  if (!response.ok) throw new Error("Failed to fetch user posts");

  return response.json();
};

export const updatePost = async (postId: string, title: string, content: string) => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("Unauthorized");

  const response = await fetch(`${API_URL}/posts/${postId}`, {
    method: "PUT",
    headers: { 
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}` 
    },
    body: JSON.stringify({ title, content }),
  });

  if (!response.ok) throw new Error("Failed to update post");

  return response.json();
};

export const deletePost = async (postId: string) => {
  const token = localStorage.getItem("token");
  if (!token) throw new Error("Unauthorized");

  const response = await fetch(`${API_URL}/posts/${postId}`, {
    method: "DELETE",
    headers: { Authorization: `Bearer ${token}` },
  });

  if (!response.ok) throw new Error("Failed to delete post");

  return response.json();
};