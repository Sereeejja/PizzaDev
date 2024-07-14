namespace PizzaDev.Models;

public class Type
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public List<PizzaType> PizzaTypes { get; set; }
}