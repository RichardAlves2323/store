import React from "react";
import type { Product } from "./ProductCard";
import ProductCard from "./ProductCard";

const products: Product[] = [
  { id: 1, name: "Camiseta", price: 49.99, description: "Camiseta confortável", stock: 10 },
  { id: 2, name: "Tênis", price: 199.99, description: "Tênis esportivo", stock: 5 },
  { id: 3, name: "Relógio", price: 299.99, description: "Relógio elegante", stock: 2 },
];

const ProductList: React.FC = () => {
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
export { products };