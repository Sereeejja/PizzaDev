using PizzaDev.Dtos;
using PizzaDev.Models;

namespace PizzaDev.Mappers;

public static class CartMappers
{
    public static CartItemDto FromCartItemToDto(this CartItem cartItem)
    {
        return new CartItemDto
        {
            Quantity = cartItem.Quantity,
            PizzaId = cartItem.PizzaId,
            CartId = cartItem.CartId,
            SizeId = cartItem.SizeId,
            TypeId = cartItem.TypeId,
        };
    }
}