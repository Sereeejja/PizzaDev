﻿using System.ComponentModel.DataAnnotations;

namespace PizzaDev.Dtos;

public class CreatePizzaRequest
{   
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string ImageUrl { get; set; } = string.Empty;
    [Required]
    public int Price { get; set; }
    [Required]
    public int Rating { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public int SizeId { get; set; }
    [Required]
    public int TypeId { get; set; }
}