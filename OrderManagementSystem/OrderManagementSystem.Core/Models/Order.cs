namespace OrderManagementSystem.Core.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public double Quantity { get; set; }
    public double TotalCost { get; set; }
}