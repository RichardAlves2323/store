using BCrypt.Net;
using Domain.Interfaces.HashPassword;

namespace Infrastructure.HashPassword;

public class HashPasswordByBcrypt : IHashPassword
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}