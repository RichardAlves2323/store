using Domain.Entities;
namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<User> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
}