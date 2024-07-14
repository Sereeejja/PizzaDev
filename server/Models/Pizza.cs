using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaDev.Models;

[Table("Pizzas")]
public class Pizza
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    
    public int Price { get; set; }
    public int Rating { get; set; }
    public int CategoryId { get; set; }
    
    public Category Category { get; set; }
    public List<PizzaSize> PizzaSizes { get; set; } = new List<PizzaSize>();
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    public List<PizzaType> PizzaTypes { get; set; } = new List<PizzaType>();
}