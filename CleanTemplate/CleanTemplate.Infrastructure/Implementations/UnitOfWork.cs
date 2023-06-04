using CleanTemplate.Core.Abstractions;
using CleanTemplate.Core.Entities;
using CleanTemplate.UI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace CleanTemplate.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext dbContext, ILogger logger)
        {
            _context = dbContext;
            _logger = logger;
            _repositories = new Hashtable();
        }

        public async Task<int> Commit()
        {
            try
            {
                var changes = await _context.SaveChangesAsync();
                return changes;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error saving changes to the database");
                return 0;
            }
        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context, _logger);

                _repositories.Add(type, repositoryInstance);
            }


            return (IRepository<T>)_repositories[type];
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
