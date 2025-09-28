import React, { useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { products } from "./ProductList";

const ProductDetail: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const product = products.find((p) => p.id === Number(id));

  const [quantity, setQuantity] = useState(1);

  if (!product) {
    return <div className="p-8 text-center">Produto não encontrado</div>;
  }

  const handleBuy = () => {
    alert(`Você comprou ${quantity} unidade(s) de ${product.name}!`);
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
        <p className="text-gray-500 mt-1 text-center">Estoque disponível: {product.stock}</p>

        <div className="mt-4 flex items-center gap-4">
          <input
            type="number"
            min={1}
            max={product.stock}
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