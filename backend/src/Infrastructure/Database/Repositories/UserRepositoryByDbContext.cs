using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

    public class UserRepositoryByDbContext : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepositoryByDbContext(AppDbContext context)
        {
            _context = context;
        }

    public Task<User> AddAsync(User user)
    {
        var usr = _context.Users.Add(user);
        _context.SaveChangesAsync();
        return Task.FromResult(usr.Entity);
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}