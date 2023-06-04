using CleanTemplate.Core.Entities;

namespace CleanTemplate.Core.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> Commit();
    }
}
