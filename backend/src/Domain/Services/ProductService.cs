using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    private readonly IStockService _stockService;


    public ProductService(IProductRepository productRepository, IStockService stockService)
    {
        _productRepository = productRepository;
        _stockService = stockService;
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

    public async Task<Product> AddAsync(Product product)
    {
        var prod = await _productRepository.AddAsync(product);

        await _stockService.AddAsync(new Stock(prod.Id, 0));

        return prod;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        var prod = await _productRepository.UpdateAsync(product);
        return prod;
    }

    public Task DeleteAsync(int id)
    {
        return _productRepository.DeleteAsync(id);
    }
}