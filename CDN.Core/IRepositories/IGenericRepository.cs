using CDN.Core.Entities;

namespace CDN.Core.IRepositories;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);
}
