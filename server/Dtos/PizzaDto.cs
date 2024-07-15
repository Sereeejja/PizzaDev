namespace PizzaDev.Dtos;

public class PizzaDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int Price { get; set; }
    public int Rating { get; set; }
    public int Category { get; set; }

    public List<int> Sizes { get; set; } = new List<int>();
    public List<int> Types { get; set; } = new List<int>();
}