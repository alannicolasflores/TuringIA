import { useEffect } from "react";
import { refreshToken } from "../services/auth";

const useRefreshToken = () => {
    useEffect(() => {
        const token = localStorage.getItem("refreshToken");
        if (token) {
            refreshToken(token)
                .then((newToken) => localStorage.setItem("token", newToken))
                .catch((err) => console.error("Error al refrescar el token:", err));
        }
    }, []);
};

export default useRefreshToken;
