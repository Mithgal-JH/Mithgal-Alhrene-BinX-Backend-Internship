public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }   // Foreign Key
    public string ProductName { get; set; } = "";
    public decimal Price { get; set; }
}