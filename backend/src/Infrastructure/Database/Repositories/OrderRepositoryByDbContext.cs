using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class OrderRepositoryByDbContext : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepositoryByDbContext(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }

    public async Task<Order> AddAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public Task<IEnumerable<Order>> GetByProductIdAsync(int productId)
    {
        return _context.Orders
            .Where(o => o.ProductId == productId)
            .ToListAsync()
            .ContinueWith(t => (IEnumerable<Order>)t.Result);
    }
}