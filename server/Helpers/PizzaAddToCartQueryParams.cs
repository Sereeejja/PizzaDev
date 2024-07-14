using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PizzaDev.Helpers;

public class PizzaAddToCartQueryParams
{
    [Required]
    public int SizeId { get; set; }
    [Required]
    public int TypeId { get; set; }
}