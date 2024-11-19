using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AccountMgmt.Domain.Interfaces.Repositories.Generic
{
    public interface IReadRepository<T> where T : class
    {
        Task<T?> GetByIdAsync<TId>(TId id) where TId : notnull;
        Task<T?> GetByIdAsync<TId>(object[] ids) where TId : notnull;

        T? GetById<TId>(TId id) where TId : notnull;
        T? GetById<TId>(object[] ids) where TId : notnull;

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? take = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? take = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<IEnumerable<T>> GetAllAsNoTrackingAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? take = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        T Single(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<T> SingleAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<T> SingleAsNoTrackingAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        T? SingleOrDefault(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<T?> SingleOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<T?> SingleOrDefaultAsNoTrackingAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        T First(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<T> FirstAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        T? FirstOrDefault(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<T?> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        // Extras

        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
        int Count(Expression<Func<T, bool>>? predicate = null);

        Task<decimal?> SumAsync(Expression<Func<T, bool>>? predicate = null);
        decimal? Sum(Expression<Func<T, bool>>? predicate = null);

        Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null);
        bool Any(Expression<Func<T, bool>>? predicate = null);
    }
}
