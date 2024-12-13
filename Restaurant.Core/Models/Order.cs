using Restaurant.Core.Models;

public class Order
{
    public int Id { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public float TotalAmount { get; set; }    
    public DateTime Date { get; set; }
}
