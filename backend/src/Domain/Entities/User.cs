namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }

    public string role { get; set; } = "client";
    public string Password { get; set; }

    public User() { }

    public User( string email, string password)
    {
        
        Email = email;
        Password = password;
    }

    public User(int id, string email, string password)
        : this( email, password)
    {
        Id = id;
    }
}