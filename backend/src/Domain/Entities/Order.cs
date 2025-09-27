namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int ProductId { get; set; }

    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    public Order() { }

    public Order(int userId, int productId, int quantity,  DateTime orderDate)
    {
        UserId = userId;
        ProductId = productId;
        Quantity = quantity;
        OrderDate = orderDate;

    }

    public Order(int id, int userId, int productId, int quantity, DateTime orderDate, decimal totalAmount)
        : this(userId, productId, quantity, orderDate)
    {
        Id = id;
        TotalAmount = totalAmount;
    }
}