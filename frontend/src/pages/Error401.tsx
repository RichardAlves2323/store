import React from "react";
import { Link } from "react-router-dom";

const Error401: React.FC = () => {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100 p-6">
      <div className="bg-white rounded-xl shadow-lg p-10 text-center max-w-md">
        <h1 className="text-6xl font-bold text-yellow-600 mb-4">401</h1>
        <h2 className="text-2xl font-semibold mb-4">Não Autorizado</h2>
        <p className="mb-6">Você precisa estar logado para acessar esta página.</p>
        <Link
          to="/login"
          className="inline-block bg-green-600 text-white px-6 py-2 rounded hover:bg-green-700 transition"
        >
          Ir para Login
        </Link>
      </div>
    </div>
  );
};

export default Error401;