namespace PizzaDev.Helpers;

public class PizzaQueryParams
{
    public string SortBy { get; set; } = string.Empty;
    public int? Category { get; set; }
    public string Search { get; set; } = string.Empty;
    public string Order { get; set; } = "asc";
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 3;
}