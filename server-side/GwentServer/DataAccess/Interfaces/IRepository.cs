using DataAccess.Entities;

namespace DataAccess.Interfaces;

public interface IRepository<T> where T : EntityBase
{
    public Task<T?> GetByIdAsync(int id);
    public Task<T?> AddAsync(T entity);
    public Task DeleteAsync(T entity);
    public Task UpdateAsync(T entity);
}
