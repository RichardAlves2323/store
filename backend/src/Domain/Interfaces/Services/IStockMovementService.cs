using Domain.Entities;
namespace Domain.Interfaces.Services;

public interface IStockMovementService
{
    Task<StockMovement?> GetByIdAsync(int id);
    Task<IEnumerable<StockMovement>> GetByProductIdAsync(int productId);
    Task<StockMovement> AddAsync(StockMovement stockMovement);
}