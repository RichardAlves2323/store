using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services;

public class StockMovementService : IStockMovementService
{
    private readonly IStockMovementRepository _stockMovementRepository;

    private readonly IStockService _stockService;

    public StockMovementService(IStockMovementRepository stockMovementRepository, IStockService stockService)
    {
        _stockMovementRepository = stockMovementRepository;
        _stockService = stockService;
    }

    public async Task<StockMovement?> GetByIdAsync(int id)
    {
        return await _stockMovementRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<StockMovement>> GetByProductIdAsync(int productId)
    {
        return await _stockMovementRepository.GetByProductIdAsync(productId);
    }

    public async Task<StockMovement> AddAsync(StockMovement stockMovement)
    {

        if (stockMovement.Quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }

        var stockM = await _stockMovementRepository.AddAsync(stockMovement);

        await UpdateStockAsync(stockM);
        return stockM;
    }
    
    private async Task UpdateStockAsync(StockMovement stockMovement)
    {
        var stock = await _stockService.GetByProductIdAsync(stockMovement.ProductId);
        if (stock != null)
        {
            if (stockMovement.Type == MovementType.IN)
            {
                stock.Quantity += stockMovement.Quantity;
            }
            else
            {
                stock.Quantity -= stockMovement.Quantity;
            }
            
            if (stock.Quantity < 0)
            {
                throw new InvalidOperationException("Stock quantity cannot be negative.");
            }

            await _stockService.UpdateAsync(stock);
        }
        else if (stockMovement.Type == MovementType.IN)
        {
            var newStock = new Stock(productId: stockMovement.ProductId, quantity: stockMovement.Quantity);

            await _stockService.AddAsync(newStock);
        }
    }
}