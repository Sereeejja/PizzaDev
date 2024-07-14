using Microsoft.EntityFrameworkCore;
using PizzaDev.Data;
using PizzaDev.Interfaces.Repository;

namespace PizzaDev.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;
    
    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public async Task<bool> ExistsAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity != null;
    }

    public async Task<T> CreateAsync(T model)
    {
        var entity = await _dbSet.AddAsync(model);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }
}