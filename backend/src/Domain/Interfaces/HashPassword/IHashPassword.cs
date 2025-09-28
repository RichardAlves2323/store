using Domain.Entities;

namespace Domain.Interfaces.HashPassword;

public interface IHashPassword
{
    string Hash(string password);
    bool Verify(string password, string hashedPassword);
}