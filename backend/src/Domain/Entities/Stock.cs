namespace Domain.Entities;

public class Stock
{
    public int Id { get; set; }
    public int ProductId { get; set; }

    public Product? Product { get; set; }
    public int Quantity { get; set; }

    public Stock() { }

    public Stock(int productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public Stock(int id, int productId, int quantity)
        : this(productId, quantity)
    {
        Id = id;
    }
}