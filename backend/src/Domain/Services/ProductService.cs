using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;


    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }   

    
    public Task<Product?> GetByIdAsync(int id)
    {
        var product = _productRepository.GetByIdAsync(id);
        return product;
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = _productRepository.GetAllAsync();
        return products;
    }

    public Task<Product> AddAsync(Product product)
    {
        var prod = _productRepository.AddAsync(product);
        return prod;
    }

    public Task<Product> UpdateAsync(Product product)
    {
        var prod = _productRepository.UpdateAsync(product);
        return prod;
    }

    public Task DeleteAsync(int id)
    {
        return _productRepository.DeleteAsync(id);
    }
}