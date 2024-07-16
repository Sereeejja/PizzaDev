using Microsoft.EntityFrameworkCore;
using PizzaDev.Data;
using PizzaDev.Interfaces;
using PizzaDev.Interfaces.Repository;
using PizzaDev.Models;

namespace PizzaDev.Repository;

public class PizzaRepository : IPizzaRepository
{
    private readonly ApplicationDbContext _context;
    
    public PizzaRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }
    public IQueryable<Pizza> GetAll()
    {
        return _context.Pizzas.AsQueryable();
    }

    public async Task<Pizza?> GetByIdAsync(int id)
    {
        return await _context.Pizzas.FindAsync(id);
    }

    public async Task<Pizza?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Pizzas
            .Include(p => p.PizzaSizes)
            .ThenInclude(ps => ps.Size)
            .Include(p => p.PizzaTypes)
            .ThenInclude(pt => pt.Type)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pizza> CreateAsync(Pizza pizzaModel)
    {
        var createdPizza = await _context.Pizzas.AddAsync(pizzaModel);
        await _context.SaveChangesAsync();
        return createdPizza.Entity;
    }

    public async Task DeleteAsync(Pizza pizza)
    {
        _context.Pizzas.Remove(pizza);
        await _context.SaveChangesAsync();
    }

    public async Task CreatePizzaSizeAsync(PizzaSize pizzaSize)
    {
        await _context.PizzaSizes.AddAsync(pizzaSize);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}