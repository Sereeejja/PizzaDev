using PizzaDev.Dtos;
using PizzaDev.Helpers;
using PizzaDev.Models;

namespace PizzaDev.Interfaces.Service;

public interface IPizzaService
{
    /* Pizza */
    public Task<(List<PizzaDto>, int pages)> GetAllAsync(PizzaQueryParams queryParams);
    public Task<Pizza?> GetOneAsync(int pizzaId);
    public Task<Pizza?> CreateAsync(CreatePizzaRequest request);
    public Task<bool> DeleteAsync(int pizzaId);
    public Task<Pizza?> EditAsync(int pizzaId, EditPizzaRequest request);
    
    /* Pizza Size */
    public Task<PizzaSize?> AddSizeAsync(int pizzaId, int sizeId);
}