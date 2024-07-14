namespace PizzaDev.Models;

public class Cart
{
    public int Id { get; set; }
    public int TotalPrice { get; set; }

    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
}