using CleanTemplate.Core.Abstractions;
using CleanTemplate.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanTemplate.Infrastructure.Implementations
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query,
                               (current, include) => current.Include(include));

            query = spec.IncludeStrings.Aggregate(query,
                (current, include) => current.Include(include));

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            if (spec.GroupBy != null)
            {
                query = query.GroupBy(spec.GroupBy).SelectMany(x => x);
            }

            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip)
                             .Take(spec.Take);
            }
            return query;
        }
    }
}
