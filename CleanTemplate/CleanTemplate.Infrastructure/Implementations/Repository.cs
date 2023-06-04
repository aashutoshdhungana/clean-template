using CleanTemplate.Core.Abstractions;
using CleanTemplate.Core.Entities;
using CleanTemplate.UI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanTemplate.Infrastructure.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public Repository(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public Task<int> CountAsync(ISpecification<T> spec)
        {
            var count = SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec).CountAsync();
            return count;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> FirstAsync(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec).FirstAsync();   
        }

        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec).FirstOrDefaultAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec).ToListAsync();
        }

        public async Task<bool> UpdateAsync(int id, T entity)
        {
            var dbEntity = await _context.FindAsync<T>(id);
            if (dbEntity == null)
                return false;

            _context.Entry(dbEntity).CurrentValues.SetValues(entity);
            return true;   
        }
    }
}
