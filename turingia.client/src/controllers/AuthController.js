import axios from "axios";

// Configuraci�n base para Axios
const API_URL = "http://localhost:5000/api/auth"; // Cambia a tu URL base de API

/**
 * Inicia sesi�n en la aplicaci�n y obtiene el token de acceso.
 * @param {Object} loginData - Contiene las credenciales del usuario { email, password }.
 * @returns {Object} - Token de acceso y datos del usuario si la autenticaci�n es exitosa.
 * @throws {Error} - Error con el mensaje de la respuesta si ocurre un problema.
 */
export const login = async (loginData) => {
    try {
        const response = await axios.post(`${API_URL}/login`, loginData);
        return response.data; // Devuelve el token y dem�s datos del backend
    } catch (error) {
        console.error("Error en el inicio de sesi�n:", error.response?.data?.message || error.message);
        throw new Error(error.response?.data?.message || "Error al iniciar sesi�n");
    }
};

/**
 * Refresca el token de acceso utilizando el refreshToken.
 * @param {string} refreshToken - Token de actualizaci�n v�lido.
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
 * Cierra la sesi�n del usuario actual.
 * @returns {void} - Confirmaci�n del cierre de sesi�n.
 * @throws {Error} - Error con el mensaje de la respuesta si ocurre un problema.
 */
export const logout = async () => {
    try {
        await axios.post(`${API_URL}/logout`);
        console.log("Sesi�n cerrada con �xito");
    } catch (error) {
        console.error("Error al cerrar la sesi�n:", error.response?.data?.message || error.message);
        throw new Error(error.response?.data?.message || "Error al cerrar sesi�n");
    }
};
