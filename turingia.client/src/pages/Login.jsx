import React from "react";
import LoginForm from "../components/Auth/LoginForm";
import "../styles/LoginPage.css"; // Importamos los estilos espec�ficos de la p�gina

const Login = () => {
    const handleLogin = (data) => {
        console.log("Datos del formulario:", data);
        // Aqu� puedes manejar la l�gica del inicio de sesi�n (llamada a API, etc.)
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
