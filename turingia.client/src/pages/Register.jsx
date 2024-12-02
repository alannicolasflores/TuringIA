import React from "react";
import RegisterForm from "../components/Auth/RegisterForm";
import { register } from "../services/auth";
import { useNavigate } from "react-router-dom";

const Register = () => {
    const navigate = useNavigate();

    const handleRegister = async (userData) => {
        try {
            await register(userData);
            alert("Usuario registrado exitosamente");
            navigate("/login");
        } catch (error) {
            alert(error.message);
        }
    };

    return <RegisterForm onSubmit={handleRegister} />;
};

export default Register;
