namespace Domain.Entities;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public Product() { }    
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