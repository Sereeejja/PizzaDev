namespace PizzaDev.Interfaces.Repository;

public interface IGenericRepository<T> where T : class
{
    Task<bool> ExistsAsync(int id);
    Task<T> CreateAsync(T model);
}