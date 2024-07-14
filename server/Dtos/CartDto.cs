namespace PizzaDev.Dtos;

public class CartDto
{
    public int Id { get; set; }
    public int TotalPrice { get; set; }

    public List<PizzaCartDto> Pizzas { get; set; } 
}