using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetByUserIdAsync(int userId);

    Task<IEnumerable<Order>> GetByProductIdAsync(int productId);
    Task<Order> AddAsync(Order order);
}