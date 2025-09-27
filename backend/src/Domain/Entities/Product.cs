namespace Domain.Entities;
public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    public Product(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
    
    public Product(int id, string name, string description, decimal price)
        : this(name, description, price)
    {
        Id = id;
    }
}