namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }

    public string Role { get; set; } = "client";
    public string Password { get; set; }

    public User() { }

    public User(string email, string password, string role = "client")
    {

        Email = email;
        Password = password;
        Role = role;
    }

    public User(int id, string email, string password, string role = "client")
        : this( email, password, role)
    {
        Id = id;
    }
}