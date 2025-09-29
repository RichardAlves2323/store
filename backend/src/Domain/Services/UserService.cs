using Domain.Entities;
using Domain.Interfaces.HashPassword;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    private readonly IHashPassword _hashPassword;

    public UserService(IUserRepository userRepository, IHashPassword hashPassword)
    {
        _userRepository = userRepository;
        _hashPassword = hashPassword;
    }   

    public async Task<User?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user != null)
        {
            var userWithoutPassword = new User(id: user.Id, email: user.Email);
            return userWithoutPassword;
        }
        return user;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user != null)
        {
            var userWithoutPassword = new User(id: user.Id, email: user.Email);
            return userWithoutPassword;
        }
        return user;
    }

    public async Task<User> AddAsync(User user)
    {
        var existUser = await _userRepository.GetByEmailAsync(user.Email);

        if (existUser != null)
        {
            throw new Exception("User with this email already exists.");
        }

        user.Password = _hashPassword.Hash(user.Password);

        var createdUser = await _userRepository.AddAsync(user);

        var userWithoutPassword = new User(id: createdUser.Id, email: createdUser.Email);

        return userWithoutPassword;
    }
}