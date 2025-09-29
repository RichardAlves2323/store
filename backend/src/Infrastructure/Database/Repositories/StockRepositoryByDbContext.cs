using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class StockRepositoryByDbContext : IStockRepository
{
    private readonly AppDbContext _context;

    public StockRepositoryByDbContext(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Stock?> GetByProductIdAsync(int productId)
    {
        return await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == productId);
    }

    public async Task<Stock> AddAsync(Stock stock)
    {
        _context.Stocks.Add(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock> UpdateAsync(Stock stock)
    {
        _context.Stocks.Update(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public Task<IEnumerable<Stock>> GetAllAsync()
    {
        var stocks = _context.Stocks.Include(s => s.Product).AsEnumerable();

        Console.WriteLine("Retrieved stocks from database:");
        foreach (var stock in stocks)
        {
            Console.WriteLine($"Stock ID: {stock.Id}, Product ID: {stock.Product}, Quantity: {stock.Quantity}");
        }   
        return Task.FromResult(stocks);
    }
}