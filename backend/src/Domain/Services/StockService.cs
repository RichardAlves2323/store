using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services;

public class StockService : IStockService
{
    private readonly IStockRepository _stockRepository;

    public StockService(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<Stock?> GetByProductIdAsync(int productId)
    {
        return await _stockRepository.GetByProductIdAsync(productId);
    }

    public async Task<Stock> AddAsync(Stock stock)
    {
        return await _stockRepository.AddAsync(stock);
    }

    public async Task<Stock> UpdateAsync(Stock stock)
    {
        return await _stockRepository.UpdateAsync(stock);
    }
}