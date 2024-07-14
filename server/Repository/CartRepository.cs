using Microsoft.EntityFrameworkCore;
using PizzaDev.Data;
using PizzaDev.Interfaces;
using PizzaDev.Interfaces.Repository;
using PizzaDev.Models;

namespace PizzaDev.Repository;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;
    
    public CartRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }
    public async Task<Cart?> GetCartByIdAsync(int cartId)
    {
        return await _context.Carts.FindAsync(cartId);
        /*return await _context.Carts.Where(c => c.Id == cartId).AsQueryable();*/
    }

    public async Task<Cart?> GetCartByIdWithDetailsAsync(int cartId)
    {
        return await _context.Carts.Include(c => c.CartItems)
            .ThenInclude(ci => ci.Pizza)
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Size)
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Type)
            .FirstOrDefaultAsync(c => c.Id == cartId);
        /*.ThenInclude(ci => ci.Pizza)
        .ThenInclude(p => p.PizzaSizes)
        .ThenInclude(ps => ps.Size)
        .Include(c => c.CartItems)
        .ThenInclude(ci => ci.Pizza)
        .ThenInclude(p => p.PizzaTypes)
        .FirstOrDefaultAsync(c => c.Id == cartId);*/
    }

    public async Task<CartItem?> AddItemAsync(CartItem cartItem)
    {
        await _context.CartItems.AddAsync(cartItem);
        await _context.SaveChangesAsync();
        return cartItem;
    }

    public async Task RemoveItemAsync(CartItem cartItem)
    {
        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task<CartItem?> GetCartItem(int pizzaId, int cartId, int sizeId, int typeId)
    {
        return await _context.CartItems
            .FirstOrDefaultAsync(ci => 
                                       ci.CartId == cartId && 
                                       ci.PizzaId == pizzaId && 
                                       ci.SizeId == sizeId && 
                                       ci.TypeId == typeId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}