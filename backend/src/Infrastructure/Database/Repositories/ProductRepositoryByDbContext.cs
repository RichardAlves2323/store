
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Database.Repositories;

    public class ProductRepositoryByDbContext : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepositoryByDbContext(AppDbContext context)
        {
            _context = context;
        }

    public Task<Product> AddAsync(Product product)
    {
        var prod = _context.Products.Add(product);
        _context.SaveChangesAsync();
        return Task.FromResult(prod.Entity);
    }

    public Task DeleteAsync(int id)
    {   
        var product = _context.Products.Find(id);
        
        if (product == null) return Task.CompletedTask;

        _context.Products.Remove(product);
        _context.SaveChangesAsync();
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = _context.Products.AsEnumerable();
        return Task.FromResult(products);
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        var product = _context.Products.Find(id);
        return Task.FromResult(product);
    }

    public Task<Product> UpdateAsync(Product product)
    {
        product = _context.Products.Update(product).Entity;
        _context.SaveChangesAsync();
        return Task.FromResult(product);
    }

    
}


