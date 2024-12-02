import React from "react";
import { Routes, Route } from "react-router-dom";
import Login from "./pages/Login";
import Register from "./pages/Register";
import "./styles/global.css"; // Importar los estilos globales
const App = () => {
    return (
        <Routes>
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/" element={<h1>Bienvenido a la aplicación</h1>} />
        </Routes>
    );
};

export default App;
