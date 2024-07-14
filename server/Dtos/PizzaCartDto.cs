using System.Drawing;

namespace PizzaDev.Dtos;

public class PizzaCartDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int Price { get; set; }
    public int Rating { get; set; }
    public int Category { get; set; }
    public int Count { get; set; }
    public int Size { get; set; }
    public string Type { get; set; }
    /*public List<int> Sizes { get; set; }
    public List<int> Types { get; set; }*/

}