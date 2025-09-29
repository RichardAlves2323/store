import React from "react";
import { Link } from "react-router-dom";

const Error403: React.FC = () => {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100 p-6">
      <div className="bg-white rounded-xl shadow-lg p-10 text-center max-w-md">
        <h1 className="text-6xl font-bold text-red-600 mb-4">403</h1>
        <h2 className="text-2xl font-semibold mb-4">Acesso Negado</h2>
        <p className="mb-6">Você não tem permissão para acessar esta página.</p>
        <Link
          to="/"
          className="inline-block bg-blue-600 text-white px-6 py-2 rounded hover:bg-blue-700 transition"
        >
          Voltar para o início
        </Link>
      </div>
    </div>
  );
};

export default Error403;