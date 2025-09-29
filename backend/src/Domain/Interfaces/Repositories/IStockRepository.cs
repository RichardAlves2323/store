using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IStockRepository
{
    Task<Stock?> GetByProductIdAsync(int productId);
    Task<IEnumerable<Stock>> GetAllAsync();
    Task<Stock> AddAsync(Stock stock);
    Task<Stock> UpdateAsync(Stock stock);
}