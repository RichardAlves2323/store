import React from "react";
import { useNavigate } from "react-router-dom";

export interface Product {
  id: number;
  name: string;
  price: number;
  description: string;
  stock: number;
  imageUrl?: string;
}

interface ProductCardProps {
  product: Product;
}

const ProductCard: React.FC<ProductCardProps> = ({ product }) => {
  const navigate = useNavigate();

  return (
    <div
      className="bg-white rounded-lg shadow-md hover:shadow-xl transition-shadow duration-300 cursor-pointer overflow-hidden"
      onClick={() => navigate(`/product/${product.id}`)}
    >
      <img
        src={product.imageUrl || "https://radio93fm.com.br/wp-content/uploads/2019/02/produto-585x380.png"}
        alt={product.name}
        className="w-full h-48 object-cover"
      />
      <div className="p-4">
        <h3 className="text-lg font-semibold">{product.name}</h3>
        <p className="text-gray-600 mt-2">${product.price.toFixed(2)}</p>
      </div>
    </div>
  );
};

export default ProductCard;