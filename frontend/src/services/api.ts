import axios from "axios";

// 👉 URL base da sua API
const api = axios.create({
  baseURL: `${import.meta.env.VITE_API_URL}/api`,
});


// eslint-disable-next-line @typescript-eslint/no-explicit-any
api.interceptors.request.use((config: any) => {
  const token = localStorage.getItem("token");
  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;