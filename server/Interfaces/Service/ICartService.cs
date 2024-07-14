using PizzaDev.Dtos;
using PizzaDev.Helpers;
using PizzaDev.Models;

namespace PizzaDev.Interfaces.Service;

public interface ICartService
{
    public Task<CartDto> GetCartAsync(int cartId);
    public Task<CartItem?> AddItemAsync(int pizzaId, PizzaAddToCartQueryParams reqParams);
    public Task<bool> RemoveItemAsync(int pizzaId, bool removeAll, PizzaAddToCartQueryParams reqParams);
    public Task<bool> ClearCartAsync(int cartId);
}