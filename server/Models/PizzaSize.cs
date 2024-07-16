namespace PizzaDev.Models;

public class PizzaSize
{
    public int PizzaId { get; set; }
    public Pizza Pizza { get; set; } 
    
    public int SizeId { get; set; }
    public Size Size { get; set; } 
}