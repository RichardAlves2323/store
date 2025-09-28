using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

public static class SeedData
{
    public static async Task EnsureAdminUser(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.MigrateAsync();

        if (!await context.Users.AnyAsync(u => u.Role.ToString() == "Admin"))
        {
            var adminUser = new User(email: "admin@gmail.com", password: "admin123", role: UserRole.Admin);
            context.Users.Add(adminUser);
            await context.SaveChangesAsync();

        }
    }
}