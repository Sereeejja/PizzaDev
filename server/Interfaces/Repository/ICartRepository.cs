using PizzaDev.Models;

namespace PizzaDev.Interfaces.Repository;

public interface ICartRepository
{
    /* Cart */
    public Task<Cart?> GetCartByIdAsync(int cartId);
    public Task<Cart?> GetCartByIdWithDetailsAsync(int cartId);
    /* Cart Item */
    public Task<CartItem?> AddItemAsync(CartItem cartItem);
    public Task RemoveItemAsync(CartItem cartItem);
    public Task<CartItem?> GetCartItem(int pizzaId, int cartId, int sizeId, int typeId);
    public Task SaveChangesAsync();

}