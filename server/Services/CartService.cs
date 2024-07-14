using PizzaDev.Dtos;
using PizzaDev.Helpers;
using PizzaDev.Interfaces;
using PizzaDev.Interfaces.Repository;
using PizzaDev.Interfaces.Service;
using PizzaDev.Mappers;
using PizzaDev.Models;

namespace PizzaDev.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepo;
    private readonly IPizzaRepository _pizzaRepo;
    
    public CartService(ICartRepository cartRepository, IPizzaRepository pizzaRepository)
    {
        _cartRepo = cartRepository;
        _pizzaRepo = pizzaRepository;
    }
    public async Task<CartDto> GetCartAsync(int cartId)
    {
        var cart = await _cartRepo.GetCartByIdWithDetailsAsync(cartId);
        
        return new CartDto
        {
            Id = cart.Id,
            TotalPrice = cart.TotalPrice,
            Pizzas = cart.CartItems
                .Select(ci => ci.Pizza.PizzaToPizzaCartDto(ci.Quantity, ci.Type.Name, int.Parse(ci.Size.Name.Replace("cm", "")))).ToList(),
        };
    }

    public async Task<CartItem?> AddItemAsync(int pizzaId, PizzaAddToCartQueryParams reqParams)
    {
        var pizza = await _pizzaRepo.GetByIdAsync(pizzaId);
        var cart = await _cartRepo.GetCartByIdAsync(1);
        if (pizza == null || cart == null) return null;

        var cartPizza = await _cartRepo.GetCartItem(pizzaId, 1, reqParams.SizeId, reqParams.TypeId);
        if (cartPizza != null)
        {
            cartPizza.Quantity += 1;
        }
        else
        {
            cartPizza = new CartItem
            {
                PizzaId = pizzaId, CartId = 1, Quantity = 1, TypeId = reqParams.TypeId, SizeId = reqParams.SizeId
            };
            await _cartRepo.AddItemAsync(cartPizza);
        }
        
        cart.TotalPrice += pizza.Price;
        await _cartRepo.SaveChangesAsync();
        return cartPizza;
    }

    public async Task<bool> RemoveItemAsync(int pizzaId, bool removeAll, PizzaAddToCartQueryParams reqParams)
    {
        var cart = await _cartRepo.GetCartByIdAsync(1);
        var pizza = await _pizzaRepo.GetByIdAsync(pizzaId);
        if (cart == null) return false;

        var cartPizza = await _cartRepo.GetCartItem(pizzaId, 1, reqParams.SizeId, reqParams.TypeId);
        if (cartPizza == null) return false;

        if (removeAll || cartPizza.Quantity == 1)
        {
            cart.TotalPrice -= cartPizza.Quantity * pizza.Price;
            await _cartRepo.RemoveItemAsync(cartPizza);

        }
        else
        {
            cart.TotalPrice -= pizza.Price;
            cartPizza.Quantity--;
        }

        await _cartRepo.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> ClearCartAsync(int cartId)
    {
        var cart = await _cartRepo.GetCartByIdWithDetailsAsync(cartId);
            /*.Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.Id == cartId);*/
        
        if (cart == null) return false;
        
        cart.TotalPrice = 0;
        
        for (int i = cart.CartItems.Count - 1;  i >= 0; i--)
        {
            var cartItem = cart.CartItems[i];
            await _cartRepo.RemoveItemAsync(cartItem);
        }

        await _cartRepo.SaveChangesAsync();
        return true;
    }
}