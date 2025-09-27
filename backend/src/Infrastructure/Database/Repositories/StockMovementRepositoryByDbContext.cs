using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class StockMovementRepositoryByDbContext : IStockMovementRepository
{
    private readonly AppDbContext _context;

    public StockMovementRepositoryByDbContext(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StockMovement?> GetByIdAsync(int id)
    {
        return await _context.StockMovements.FirstOrDefaultAsync(sm => sm.Id == id);
    }

    public async Task<IEnumerable<StockMovement>> GetByProductIdAsync(int productId)
    {
        return await _context.StockMovements
            .Where(sm => sm.ProductId == productId)
            .ToListAsync();
    }

    public async Task<StockMovement> AddAsync(StockMovement stockMovement)
    {
        _context.StockMovements.Add(stockMovement);
        await _context.SaveChangesAsync();
        return stockMovement;
    }
}