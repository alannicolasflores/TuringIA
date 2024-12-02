import axios from "axios";

const API_URL = "http://localhost:5000/api/auth";

export const login = async (credentials) => {
    const response = await axios.post(`${API_URL}/login`, credentials);
    return response.data;
};

export const register = async (userData) => {
    const response = await axios.post(`${API_URL}/register`, userData);
    return response.data;
};

export const refreshToken = async (refreshToken) => {
    const response = await axios.post(`${API_URL}/refresh-token`, { refreshToken });
    return response.data;
};

export const logout = async () => {
    await axios.post(`${API_URL}/logout`);
};

