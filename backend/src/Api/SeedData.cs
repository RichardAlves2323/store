using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using BCrypt.Net;

public static class SeedData
{
    public static async Task EnsureAdminUser(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.MigrateAsync();

        if (!await context.Users.AnyAsync(u => u.Role.ToString() == "Admin"))
        {
            var adminUser = new User(email: "admin@gmail.com", password: BCrypt.Net.BCrypt.HashPassword("admin123"), role: UserRole.Admin);
            context.Users.Add(adminUser);
            await context.SaveChangesAsync();

        }
    }
}