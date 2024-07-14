using Microsoft.EntityFrameworkCore;

namespace PizzaDev.Models;

public class CartItem
{
    public int Id { get; set; }
    
    public int CartId { get; set; }
    public int PizzaId { get; set; }
    public int SizeId { get; set; }
    public int TypeId { get; set; }
    public int Quantity { get; set; }

    public Pizza Pizza { get; set; } 
    public Type Type { get; set; }
    public Size Size { get; set; }
    public Cart Cart { get; set; }
}