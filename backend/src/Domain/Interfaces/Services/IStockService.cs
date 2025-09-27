using Domain.Entities;
namespace Domain.Interfaces.Services;

public interface IStockService
{
    Task<Stock?> GetByProductIdAsync(int productId);
    Task<Stock> AddAsync(Stock stock);
    Task<Stock> UpdateAsync(Stock stock);
}