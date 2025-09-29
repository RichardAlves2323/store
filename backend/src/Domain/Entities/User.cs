namespace Domain.Entities;


public enum UserRole
{
    Admin,
    Client
}
public class User
{
    public int Id { get; set; }
    public string Email { get; set; }

    public UserRole Role { get; set; } = UserRole.Client;
    public string Password { get; set; }

    public User() { }

    public User(int id, string email)
    {
        Id = id;
        Email = email;
    }

    public User(string email, string password, UserRole role = UserRole.Client)
    {

        Email = email;
        Password = password;
        Role = role;
    }

    public User(int id, string email, string password, UserRole role = UserRole.Client)
        : this(email, password, role)
    {
        Id = id;
    }
}