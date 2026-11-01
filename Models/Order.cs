namespace HoangHaPC.Models;

public class Order
{
    public int Id { get; set; }
    public string Code { get; set; } = "";
    public string CustomerName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string? Email { get; set; }
    public string Address { get; set; } = "";
    public string? Note { get; set; }
    public string PaymentMethod { get; set; } = "COD";
    public string Status { get; set; } = "Chờ xác nhận";
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<OrderItem> Items { get; set; } = new();
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal => UnitPrice * Quantity;
}
