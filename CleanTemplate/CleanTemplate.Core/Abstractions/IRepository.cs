using CleanTemplate.Core.Entities;

namespace CleanTemplate.Core.Abstractions
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task AddAsync(T entity);
        Task<bool> UpdateAsync(int id, T entity);
        void Delete(T entity);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<T> FirstAsync(ISpecification<T> spec);
        Task<T?> FirstOrDefaultAsync(ISpecification<T> spec);
    }
}
