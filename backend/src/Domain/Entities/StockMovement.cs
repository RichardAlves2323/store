namespace Domain.Entities;

public enum MovementType
{
    IN,
    OUT
}

public class StockMovement
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public MovementType Type { get; set; }
    public int Quantity { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public StockMovement() { }

    public StockMovement(int productId, MovementType type, int quantity)
    {
        ProductId = productId;
        Type = type;
        Quantity = quantity;
    }

    public StockMovement(int id, int productId, MovementType type, int quantity)
        : this(productId, type, quantity)
    {
        Id = id;
    }
}