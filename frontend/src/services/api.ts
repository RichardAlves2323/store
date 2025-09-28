import axios from "axios";

// 👉 URL base da sua API
const api = axios.create({
  baseURL: "http://localhost:5211/api",
});


api.interceptors.request.use((config: any) => {
  const token = localStorage.getItem("token");
  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;