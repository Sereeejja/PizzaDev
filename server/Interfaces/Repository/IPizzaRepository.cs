using PizzaDev.Models;

namespace PizzaDev.Interfaces.Repository;

public interface IPizzaRepository
{
    /* Pizza */
    public IQueryable<Pizza> GetAll();
    public Task<Pizza?> GetByIdAsync(int id);
    public Task<Pizza> CreateAsync(Pizza pizzaModel);
    public Task DeleteAsync(Pizza pizza);
    
    /* Pizza Size */
    public Task CreatePizzaSizeAsync(PizzaSize pizzaSize);
    
    /* Save changes */
    public Task SaveChangesAsync();

}