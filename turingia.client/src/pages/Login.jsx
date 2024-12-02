import React from "react";
import LoginForm from "../components/Auth/LoginForm";
import "../styles/LoginPage.css"; // Importamos los estilos específicos de la página

const Login = () => {
    const handleLogin = (data) => {
        console.log("Datos del formulario:", data);
        // Aquí puedes manejar la lógica del inicio de sesión (llamada a API, etc.)
    };

    return (
        <div className="login-page">
            <div className="login-container">
                <LoginForm onSubmit={handleLogin} />
            </div>
        </div>
    );
};

export default Login;
