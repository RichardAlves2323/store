using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    private readonly IStockMovementService _stockMovementService;

    private readonly IProductService _productService;

    public OrderService(IOrderRepository orderRepository, IStockMovementService stockMovementService, IProductService productService)
    {
        _orderRepository = orderRepository;
        _stockMovementService = stockMovementService;
        _productService = productService;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _orderRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
    {
        return await _orderRepository.GetByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Order>> GetByProductIdAsync(int productId)
    {
        return await _orderRepository.GetByProductIdAsync(productId);
    }

    public async Task<Order> AddAsync(Order order)
    {

        if (order.Quantity <= 0)
        {
            throw new Exception("Quantity must be greater than zero");
        }

        var product = await _productService.GetByIdAsync(order.ProductId);
        if (product == null)
        {
            throw new Exception("Product not found");
        }

        var totalAmount = order.Quantity * product.Price;
        order.TotalAmount = totalAmount;

        var createdOrder = await _orderRepository.AddAsync(order);

        var stockMovement = new StockMovement(createdOrder.ProductId, MovementType.OUT, createdOrder.Quantity );

        await _stockMovementService.AddAsync(stockMovement);
        
        return createdOrder;
    }
}