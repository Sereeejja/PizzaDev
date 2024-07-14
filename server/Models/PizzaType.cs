namespace PizzaDev.Models;

public class PizzaType
{
    public int PizzaId { get; set; }
    public Pizza Pizza { get; set; }
    
    public int TypeId { get; set; }
    public Type Type { get; set; }
}