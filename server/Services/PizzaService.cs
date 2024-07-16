using Microsoft.EntityFrameworkCore;
using PizzaDev.Dtos;
using PizzaDev.Helpers;
using PizzaDev.Interfaces;
using PizzaDev.Interfaces.Repository;
using PizzaDev.Interfaces.Service;
using PizzaDev.Mappers;
using PizzaDev.Models;
using Type = PizzaDev.Models.Type;

namespace PizzaDev.Services;

public class PizzaService : IPizzaService
{
    private readonly IPizzaRepository _pizzaRepo;
    private readonly IGenericRepository<Size> _sizeRepo;
    private readonly IGenericRepository<Type> _typeRepo;
    private readonly IGenericRepository<PizzaType> _pizzaTypeRepo;
    private readonly IGenericRepository<PizzaSize> _pizzaSizeRepo;
    private readonly IGenericRepository<Category> _categoryRepo;
    
    public PizzaService(
        IPizzaRepository pizzaRepository, 
        IGenericRepository<Size> sizeRepo, 
        IGenericRepository<Type> typeRepo,
        IGenericRepository<PizzaSize> pizzaSizeRepo,
        IGenericRepository<PizzaType> pizzaTypeRepo,
        IGenericRepository<Category> categoryRepo
        )
    {
        _pizzaRepo = pizzaRepository;
        _sizeRepo = sizeRepo;
        _typeRepo = typeRepo;
        _pizzaSizeRepo = pizzaSizeRepo;
        _pizzaTypeRepo = pizzaTypeRepo;
        _categoryRepo = categoryRepo;

    }
    public async Task<(List<PizzaDto>, int pages)> GetAllAsync(PizzaQueryParams queryParams)
    {
        IQueryable<Pizza> query = _pizzaRepo.GetAll()
            .Include(p => p.PizzaSizes)
            .ThenInclude(ps => ps.Size)
            .Include(p => p.PizzaTypes);
        
        if (!string.IsNullOrEmpty(queryParams.Search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(queryParams.Search.ToLower()));
        }

        if (!string.IsNullOrEmpty(queryParams.SortBy))
        {
            query = queryParams.SortBy.ToLower() switch
            {
                "rating" => queryParams.Order == "asc" ? query.OrderBy(s => s.Rating) : query.OrderByDescending(s => s.Rating),
                "price" => queryParams.Order == "asc" ? query.OrderBy(s => s.Price) : query.OrderByDescending(s => s.Price),
                "title" => queryParams.Order == "asc" ? query.OrderBy(s => s.Name) : query.OrderByDescending(s => s.Name),
                _ => query
            };
        }

        if (queryParams.Category.HasValue)
        {
            query = query.Where(p => p.CategoryId == queryParams.Category);
        }

        var pizzas = await query.ToListAsync();
        int totalPizzas = pizzas.Count;
        int totalPages = (int)Math.Ceiling((double)totalPizzas / queryParams.Limit);
        
        var pizzaDtos = pizzas
            .Skip((queryParams.Page - 1) * queryParams.Limit)
            .Take(queryParams.Limit)
            .Select(p => p.PizzaToDto()).ToList();
        
        return (pizzaDtos, totalPages);
    }

    public async Task<Pizza?> GetOneAsync(int pizzaId)
    {
        var pizza = await _pizzaRepo.GetByIdWithDetailsAsync(pizzaId);
        return pizza;
    }

    public async Task<Pizza?> CreateAsync(CreatePizzaRequest request)
    {
        var size = await _sizeRepo.ExistsAsync(request.SizeId);
        var type = await _typeRepo.ExistsAsync(request.TypeId);
        var category = await _categoryRepo.ExistsAsync(request.CategoryId);
        
        if (!size || !type || !category) return null;
        var createdPizza = await _pizzaRepo.CreateAsync(request.FromCreateRequestToPizza());
        var pizzaType = new PizzaType { PizzaId = createdPizza.Id, TypeId = request.TypeId };
        var pizzaSize = new PizzaSize { PizzaId = createdPizza.Id, SizeId = request.SizeId };
        await _pizzaTypeRepo.CreateAsync(pizzaType);
        await _pizzaSizeRepo.CreateAsync(pizzaSize);
        return createdPizza;
    }

    public async Task<bool> DeleteAsync(int pizzaId)
    {
        var pizza = await _pizzaRepo.GetByIdAsync(pizzaId);
        if (pizza == null)
        {
            return false;
        }
        
        await _pizzaRepo.DeleteAsync(pizza);
        return true;
    }

    public async Task<Pizza?> EditAsync(int pizzaId, EditPizzaRequest request)
    {
        var pizza = await _pizzaRepo.GetByIdWithDetailsAsync(pizzaId);
        var categoryExists = await _categoryRepo.ExistsAsync(request.CategoryId);
        if (pizza == null || !categoryExists) return null;
        
        // check category
        pizza.Name = request.Name;
        pizza.Price = request.Price;
        pizza.CategoryId = request.CategoryId;
        pizza.Rating = request.Rating;
        pizza.ImageUrl = request.ImageUrl;
        
        await _pizzaRepo.SaveChangesAsync();
        return pizza;
    }

    public async Task<PizzaSize?> AddSizeAsync(int pizzaId, int sizeId)
    {
        var pizza = await _pizzaRepo.GetByIdAsync(pizzaId);
        /*var size = await _context.Sizes.FindAsync(sizeId);*/
        if (pizza == null) return null;
        
        var pizzaSize = new PizzaSize{ PizzaId = pizzaId, SizeId = sizeId };

        await _pizzaRepo.CreatePizzaSizeAsync(pizzaSize);
        
        return pizzaSize;
    }
}