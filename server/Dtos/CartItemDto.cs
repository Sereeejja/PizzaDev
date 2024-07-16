namespace PizzaDev.Dtos;

public class CartItemDto
{
    public int CartId { get; set; }
    public int PizzaId { get; set; }
    public int Quantity { get; set; }
    public int SizeId { get; set; }
    public int TypeId { get; set; }
    
}