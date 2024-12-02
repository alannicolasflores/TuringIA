import axios from "axios";

// Configuración base para Axios
const API_URL = "http://localhost:5000/api/auth"; // Cambia a tu URL base de API

/**
 * Inicia sesión en la aplicación y obtiene el token de acceso.
 * @param {Object} loginData - Contiene las credenciales del usuario { email, password }.
 * @returns {Object} - Token de acceso y datos del usuario si la autenticación es exitosa.
 * @throws {Error} - Error con el mensaje de la respuesta si ocurre un problema.
 */
export const login = async (loginData) => {
    try {
        const response = await axios.post(`${API_URL}/login`, loginData);
        return response.data; // Devuelve el token y demás datos del backend
    } catch (error) {
        console.error("Error en el inicio de sesión:", error.response?.data?.message || error.message);
        throw new Error(error.response?.data?.message || "Error al iniciar sesión");
    }
};

/**
 * Refresca el token de acceso utilizando el refreshToken.
 * @param {string} refreshToken - Token de actualización válido.
 * @returns {Object} - Nuevo token de acceso.
 * @throws {Error} - Error con el mensaje de la respuesta si ocurre un problema.
 */
export const refreshToken = async (refreshToken) => {
    try {
        const response = await axios.post(`${API_URL}/refresh-token`, { refreshToken });
        return response.data; // Devuelve el nuevo token
    } catch (error) {
        console.error("Error al refrescar el token:", error.response?.data?.message || error.message);
        throw new Error(error.response?.data?.message || "Error al refrescar el token");
    }
};

/**
 * Cierra la sesión del usuario actual.
 * @returns {void} - Confirmación del cierre de sesión.
 * @throws {Error} - Error con el mensaje de la respuesta si ocurre un problema.
 */
export const logout = async () => {
    try {
        await axios.post(`${API_URL}/logout`);
        console.log("Sesión cerrada con éxito");
    } catch (error) {
        console.error("Error al cerrar la sesión:", error.response?.data?.message || error.message);
        throw new Error(error.response?.data?.message || "Error al cerrar sesión");
    }
};
