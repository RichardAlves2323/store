import React, { useEffect } from "react";
import type { Product } from "./ProductCard";
import ProductCard from "./ProductCard";
import api from "../services/api";
import { useNavigate } from "react-router-dom";

const ProductList: React.FC = () => {

  const [products, setProducts] = React.useState<Product[]>([]);
  const navigate = useNavigate();

  const checkAuthentication = () => {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/401");
    }
  }

  const getProducts = async () => {
    try {
      const response = await api.get("/Product");
      setProducts(response.data);
    } catch (error) {
      alert("Erro ao buscar produtos");
      console.error("Erro ao buscar produtos:", error);
    }
  }

  useEffect(() => {
    checkAuthentication();  
    getProducts();
   }, []);


  return (
    <div className="min-h-screen w-screen bg-gray-100 p-8">
      <h1 className="text-3xl font-bold mb-8 text-center">Produtos</h1>
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
        {products.map((product) => (
          <ProductCard key={product.id} product={product} />
        ))}
      </div>
    </div>
  );
};

export default ProductList;