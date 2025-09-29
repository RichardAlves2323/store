import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import type { Product } from "./ProductCard";
import api from "../services/api";
import { getUserFromToken } from "../services/getUserFromToken";


interface Stock {
  productId: number;
  quantity: number;
}

interface Order {
  productId: number;
  quantity: number;
  userId: string;
  orderDate: Date;
  totalAmount: number;
}

const ProductDetail: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [product, setProduct] = useState<Product | null>(null);
  const [stock, setStock] = useState<Stock | null>(null);

  const checkAuthentication = () => {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/401");
    }
  }

  const getProductByIdAndStock = async () => {

    try {
      const response = await api.get(`/Product/${id}`);
      setProduct(response.data);

      const stockResponse = await api.get<Stock>(`/Stock/product/${id}`);
      setStock(stockResponse.data);
    } catch (error) {
      alert("Erro ao buscar produto");
      console.error("Erro ao buscar produto:", error);
    }
    
  };
  
  useEffect(() => { 
    checkAuthentication(); 
    getProductByIdAndStock();
   }, [id]);


  const [quantity, setQuantity] = useState(1);

  if (!product) {
    return <div className="p-8 text-center">Produto não encontrado</div>;
  }

  const handleBuy = async () => {

    try {
      const user = getUserFromToken();
      if (!user) {
        alert("Usuário não autenticado");
        return;
      }
  
      if (!stock || quantity > stock.quantity) {
        alert("Quantidade indisponível em estoque");
        return;
      }
  
      const order: Order = {
        productId: product.id,
        quantity,
        userId: user.unique_name,
        orderDate: new Date(),
        totalAmount: product.price * quantity,
      };
  
      api.post("/Order", order);
  
      alert("Compra realizada com sucesso!");
      navigate("/");
    } catch (error) {
      alert("Erro ao processar compra");
      console.error("Erro ao processar compra:", error);
    }
    
  };

  return (
    <div className="min-h-screen w-screen bg-gray-100 flex items-center justify-center p-4">
      <div className="bg-white rounded-lg shadow-md p-8 w-full max-w-md flex flex-col items-center">
        <button
          className="self-start mb-4 text-blue-500 hover:underline"
          onClick={() => navigate(-1)}
        >
          ← Voltar
        </button>

        <img
          src={product.imageUrl || "https://radio93fm.com.br/wp-content/uploads/2019/02/produto-585x380.png"}
          alt={product.name}
          className="w-full h-64 object-cover rounded-md mb-6"
        />

        <h2 className="text-2xl font-bold text-center">{product.name}</h2>
        <p className="text-gray-600 mt-2 text-center">{product.description}</p>
        <p className="text-lg font-semibold mt-4 text-center">
          Preço: ${product.price.toFixed(2)}
        </p>
        <p className="text-gray-500 mt-1 text-center">Estoque disponível: {stock?.quantity}</p>

        <div className="mt-4 flex items-center gap-4">
          <input
            type="number"
            min={1}
            max={stock ? stock.quantity : 1}
            value={quantity}
            onChange={(e) => setQuantity(Math.max(1, Number(e.target.value)))}
            className="border px-3 py-2 rounded-lg w-20 text-center"
          />
          <button
            onClick={handleBuy}
            className="bg-blue-500 text-white px-4 py-2 rounded-lg hover:bg-blue-600 transition"
          >
            Comprar
          </button>
        </div>
      </div>
    </div>
  );
};

export default ProductDetail;