import React, { useEffect, useState } from "react";
import api from "../services/api";
import { getUserFromToken } from "../services/getUserFromToken";
import { useNavigate } from "react-router-dom";

type Product = {
  id: number;
  name: string;
  price: number;
  description: string;
};

const AdminPage: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([
    { id: 1, name: "Notebook", price: 3500, description: "Notebook de alto desempenho" },
    { id: 2, name: "Mouse Gamer", price: 150, description: "Mouse com alta precisão" },
  ]);

  const [newName, setNewName] = useState("");
  const [newPrice, setNewPrice] = useState<number | "">("");
  const [newDesc, setNewDesc] = useState("");

  const [selectedProductId, setSelectedProductId] = useState<number | "">("");
  const [movementType, setMovementType] = useState<0 | 1>(0); // 0 = Entrada, 1 = Saída
  const [quantity, setQuantity] = useState<number>(0);

 
  const [adminEmail, setAdminEmail] = useState("");
  const [adminPassword, setAdminPassword] = useState("");

  const navigate = useNavigate();


  const checkAuthentication = () => { 
    const token = getUserFromToken();
    if (!token || token.role !== "Admin") {
      navigate("/403");
    }
  }

  const getProducts = async () => {
    try {
      const response = await api.get("/Product");
      setProducts(response.data);
    } catch (error) {
      console.error("Erro ao buscar produtos:", error);
    }
  }

  const handleAddProduct = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!newName || newPrice === "" || !newDesc) {
      alert("Preencha todos os campos do novo produto");
      return;
    }

    const nextId = products.length > 0 ? Math.max(...products.map(p => p.id)) + 1 : 1;
    const newProduct: Product = {
      id: nextId,
      name: newName,
      price: Number(newPrice),
      description: newDesc,
    };

    try {
      const response = await api.post("/Product", newProduct);

      const createdProduct = response.data as Product;
      console.log("Produto cadastrado:", createdProduct);
      setProducts([...products, createdProduct]);
      setNewName("");
      setNewPrice("");
      setNewDesc("");
      alert("Produto cadastrado!");
      
    } catch (error) {
      alert("Erro ao cadastrar produto");
      console.error("Erro ao cadastrar produto:", error);
    }

    
  };

  const handleStockMovement = async (e: React.FormEvent) => {
    e.preventDefault();
    if (selectedProductId === "" || quantity <= 0) {
      alert("Selecione um produto e informe uma quantidade maior que 0");
      return;
    }

    try {
      await api.post("/StockMovement", {
        productId: selectedProductId,
        type: movementType,
        quantity: quantity,
      });

      alert("Movimentação registrada!");
      setSelectedProductId("");
      setMovementType(0);
      setQuantity(0);
    } catch (error) {
      alert("Erro ao registrar movimentação");
      console.error("Erro ao registrar movimentação:", error);
    }
    
  };

  
  const handleCreateAdmin = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!adminEmail || !adminPassword) {
      alert("Preencha email e senha");
      return;
    }

    
    try {

      await api.post("/User", { email: adminEmail, password: adminPassword, role: 0 });

      alert(`Usuário administrador criado: ${adminEmail}`);
      setAdminEmail("");
      setAdminPassword("");
    } catch (error) {
      alert("Erro ao criar usuário administrador");
      console.error("Erro ao criar usuário administrador:", error);
    }
    
  };

  useEffect(() => {
    checkAuthentication();
    getProducts();
   }, []);

  return (
    <div className="min-h-screen bg-gray-100 flex flex-col items-center p-6 space-y-8">
      
      <div className="w-full max-w-2xl bg-white rounded-xl shadow p-6">
        <h1 className="text-2xl font-bold mb-6 text-center">Cadastrar Novo Produto</h1>

        <form onSubmit={handleAddProduct} className="space-y-4">
          <div>
            <label className="block mb-1 font-medium">Nome</label>
            <input
              value={newName}
              onChange={(e) => setNewName(e.target.value)}
              className="w-full border rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-400"
              placeholder="Nome do produto"
            />
          </div>

          <div>
            <label className="block mb-1 font-medium">Preço</label>
            <input
              type="number"
              min={0}
              value={newPrice}
              onChange={(e) => setNewPrice(Number(e.target.value))}
              className="w-full border rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-400"
              placeholder="Preço em R$"
            />
          </div>

          <div>
            <label className="block mb-1 font-medium">Descrição</label>
            <textarea
              value={newDesc}
              onChange={(e) => setNewDesc(e.target.value)}
              className="w-full border rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-400"
              placeholder="Descrição do produto"
            />
          </div>

          <button
            type="submit"
            className="w-full bg-green-600 text-white font-semibold py-2 rounded hover:bg-green-700 transition"
          >
            Cadastrar Produto
          </button>
        </form>
      </div>

      
      <div className="w-full max-w-2xl bg-white rounded-xl shadow p-6">
        <h1 className="text-2xl font-bold mb-6 text-center">Gerenciar Estoque</h1>

        <form onSubmit={handleStockMovement} className="space-y-4">
          <div>
            <label className="block mb-1 font-medium">Produto</label>
            <select
              value={selectedProductId}
              onChange={(e) => setSelectedProductId(Number(e.target.value) || "")}
              className="w-full border rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-400"
            >
              <option value="">Selecione um produto</option>
              {products.map((product) => (
                <option key={product.id} value={product.id}>
                  {product.name}
                </option>
              ))}
            </select>
          </div>

          <div>
            <label className="block mb-1 font-medium">Tipo de movimentação</label>
            <select
              value={movementType}
              onChange={(e) => setMovementType(Number(e.target.value) as 0 | 1)}
              className="w-full border rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-400"
            >
              <option value={0}>Entrada</option>
              <option value={1}>Saída</option>
            </select>
          </div>

          <div>
            <label className="block mb-1 font-medium">Quantidade</label>
            <input
              type="number"
              min={1}
              value={quantity}
              onChange={(e) => setQuantity(Number(e.target.value))}
              className="w-full border rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-400"
            />
          </div>

          <button
            type="submit"
            className="w-full bg-blue-600 text-white font-semibold py-2 rounded hover:bg-blue-700 transition"
          >
            Salvar Movimentação
          </button>
        </form>
      </div>

      
      <div className="w-full max-w-2xl bg-white rounded-xl shadow p-6">
        <h1 className="text-2xl font-bold mb-6 text-center">Criar Usuário Administrador</h1>

        <form onSubmit={handleCreateAdmin} className="space-y-4">
          <div>
            <label className="block mb-1 font-medium">Email</label>
            <input
              type="email"
              value={adminEmail}
              onChange={(e) => setAdminEmail(e.target.value)}
              className="w-full border rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-400"
              placeholder="admin@exemplo.com"
            />
          </div>

          <div>
            <label className="block mb-1 font-medium">Senha</label>
            <input
              type="password"
              value={adminPassword}
              onChange={(e) => setAdminPassword(e.target.value)}
              className="w-full border rounded px-3 py-2 focus:outline-none focus:ring focus:border-blue-400"
              placeholder="********"
            />
          </div>

          <button
            type="submit"
            className="w-full bg-purple-600 text-white font-semibold py-2 rounded hover:bg-purple-700 transition"
          >
            Criar Administrador
          </button>
        </form>
      </div>

      
      <div className="w-full max-w-4xl bg-white rounded-xl shadow p-6">
        <h2 className="text-2xl font-bold mb-4 text-center">Produtos Cadastrados</h2>
        <div className="overflow-x-auto">
          <table className="w-full table-auto border-collapse">
            <thead>
              <tr className="bg-gray-200 text-left">
                <th className="px-4 py-2 border">ID</th>
                <th className="px-4 py-2 border">Nome</th>
                <th className="px-4 py-2 border">Preço (R$)</th>
                <th className="px-4 py-2 border">Descrição</th>
              </tr>
            </thead>
            <tbody>
              {products.map((product) => (
                <tr key={product.id} className="hover:bg-gray-100">
                  <td className="px-4 py-2 border">{product.id}</td>
                  <td className="px-4 py-2 border">{product.name}</td>
                  <td className="px-4 py-2 border">{product.price.toFixed(2)}</td>
                  <td className="px-4 py-2 border">{product.description}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
};

export default AdminPage;