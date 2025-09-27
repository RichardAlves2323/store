using Domain.Entities;
namespace Domain.Interfaces.Repositories;

public interface IStockMovementRepository
{
    Task<StockMovement?> GetByIdAsync(int id);
    Task<IEnumerable<StockMovement>> GetByProductIdAsync(int productId);
    Task<StockMovement> AddAsync(StockMovement stockMovement);
}