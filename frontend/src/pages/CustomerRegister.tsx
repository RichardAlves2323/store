import React, { useState } from "react";

const CustomerRegister: React.FC = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleRegister = (e: React.FormEvent) => {
    e.preventDefault();
    if (!email || !password) {
      alert("Preencha todos os campos");
      return;
    }

    // Aqui você chamaria a API do backend para criar o cliente
    console.log({ email, password });
    alert(`Cliente cadastrado: ${email}`);

    setEmail("");
    setPassword("");
  };

  return (
    <div className="min-h-screen bg-gray-100 flex items-center justify-center p-6">
      <div className="w-full max-w-md bg-white rounded-xl shadow p-6">
        <h1 className="text-2xl font-bold mb-6 text-center">Cadastrar Cliente</h1>

        <form onSubmit={handleRegister} className="space-y-4">
          <div>
            <label className="block mb-1 font-medium">Email</label>
            <input
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              className="w-full border rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-400"
              placeholder="cliente@exemplo.com"
            />
          </div>

          <div>
            <label className="block mb-1 font-medium">Senha</label>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="w-full border rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-400"
              placeholder="********"
            />
          </div>

          <button
            type="submit"
            className="w-full bg-blue-600 text-white font-semibold py-2 rounded hover:bg-green-700 transition"
          >
            Cadastrar
          </button>
        </form>
      </div>
    </div>
  );
};

export default CustomerRegister;