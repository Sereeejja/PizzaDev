using PizzaDev.Dtos;
using PizzaDev.Models;

namespace PizzaDev.Mappers;

public static class PizzaMappers
{
    public static PizzaDto PizzaToDto(this Pizza pizza)
    {
        return new PizzaDto
        {
            Id = pizza.Id,
            Title = pizza.Name,
            Price = pizza.Price,
            Rating = pizza.Rating,
            ImageUrl = pizza.ImageUrl
        };
    }

    public static PizzaSizeDto PizzaSizeToDto(this PizzaSize pizzaSize)
    {
        return new PizzaSizeDto
        {
            PizzaId = pizzaSize.PizzaId,
            SizeId = pizzaSize.SizeId
        };
    }
    
    public static PizzaCartDto PizzaToPizzaCartDto(this Pizza pizza, int quantity, string typeName, int size)
    {
        return new PizzaCartDto
        {
            Id = pizza.Id,
            Name = pizza.Name,
            Price = pizza.Price,
            Rating = pizza.Rating,
            ImageUrl = pizza.ImageUrl,
            Category = pizza.CategoryId,
            Count = quantity,
            Type = typeName,
            Size = size,
        };
    }

    public static Pizza FromCreateRequestToPizza(this CreatePizzaRequest request)
    {
        return new Pizza
        {
            Price = request.Price,
            Rating = request.Rating,
            ImageUrl = request.ImageUrl,
            CategoryId = request.CategoryId,
            Name = request.Name,
        };
    }
}