using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Data;
using System.Linq.Expressions;

namespace AccountMgmt.Infraestructure.Persistence.Repositories;

public abstract class GenericRepository<T> where T : class
{
    protected readonly AccountMgmtContext _context;
    protected readonly IDbConnection _connection;

    public GenericRepository(AccountMgmtContext context)
    {
        _context = context;
    }

    protected IQueryable<T> PrepareQuery(
        IQueryable<T> query,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? take = null
    )
    {
        if (include != null)
            query = include(query);

        if (predicate != null)
            query = query.Where(predicate);

        if (orderBy != null)
            query = orderBy(query);

        if (take.HasValue)
            query = query.Take(Convert.ToInt32(take));

        return query;
    }

    #region Find
    public virtual async Task<T?> GetByIdAsync<TId>(TId id) where TId : notnull
    {
        return await _context.Set<T>().FindAsync(new object[] { id });
    }

    public virtual async Task<T?> GetByIdAsync<TId>(object[] ids) where TId : notnull
    {
        return await _context.Set<T>().FindAsync(ids);
    }

    public virtual T? GetById<TId>(TId id) where TId : notnull
    {
        return _context.Set<T>().Find(new object[] { id });
    }

    public virtual T? GetById<TId>(object[] ids) where TId : notnull
    {
        return _context.Set<T>().Find(ids);
    }
    #endregion

    #region Extras
    public virtual async Task<decimal?> SumAsync(
        Expression<Func<T, bool>> predicate = null
    )
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate);

        return await ((IQueryable<decimal?>)query).SumAsync();
    }

    public virtual decimal? Sum(
        Expression<Func<T, bool>> predicate = null
    )
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate);

        return ((IQueryable<decimal?>)query).Sum();
    }

    public virtual async Task<int> CountAsync(
        Expression<Func<T, bool>> predicate = null
    )
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate);

        return await query.CountAsync();
    }

    public virtual int Count(Expression<Func<T, bool>> predicate = null)
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate);

        return query.Count();
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate);

        return await query.AnyAsync();
    }


    public virtual bool Any(Expression<Func<T, bool>> predicate = null)
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate);

        return query.Any();
    }
    #endregion

    #region Get All
    public virtual IEnumerable<T> GetAll(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? take = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    )
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate, include, orderBy, take);

        return query.ToList();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? take = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    )
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate, include, orderBy, take);

        return await query.ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsNoTrackingAsync(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        int? take = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    )
    {
        var query = _context.Set<T>().AsNoTracking().AsQueryable();

        query = PrepareQuery(query, predicate, include, orderBy, take);

        return await query.ToListAsync();
    }
    #endregion

    #region Paged
    //public virtual async Task<DataCollection<T>> GetPagedAsync(
    //    int page,
    //    int take,
    //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
    //    Expression<Func<T, bool>> predicate = null,
    //    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    //)
    //{
    //    var query = _context.Set<T>().AsQueryable();
    //    var originalPages = page;

    //    page--;

    //    if (page > 0)
    //        page = page * take;

    //    query = PrepareQuery(query, predicate, include, orderBy);

    //    var result = new DataCollection<T>
    //    {
    //        Items = await query.Skip(page).Take(take).ToListAsync(),
    //        Total = await query.CountAsync(),
    //        Page = originalPages,
    //        Take = take
    //    };

    //    if (result.Total > 0)
    //    {
    //        result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
    //    }

    //    return result;
    //}

    //public virtual DataCollection<T> GetPaged(
    //    int page,
    //    int take,
    //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
    //    Expression<Func<T, bool>> predicate = null,
    //    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    //)
    //{
    //    var query = _context.Set<T>().AsQueryable();
    //    var originalPages = page;

    //    page--;

    //    if (page > 0)
    //        page = page * take;

    //    query = PrepareQuery(query, predicate, include, orderBy);

    //    var result = new DataCollection<T>
    //    {
    //        Items = query.Skip(page).Take(take).ToList(),
    //        Total = query.Count(),
    //        Page = originalPages,
    //        Take = take
    //    };

    //    if (result.Total > 0)
    //    {
    //        result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
    //    }

    //    return result;
    //}
    #endregion

    #region First
    public virtual T First(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    )
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate, include, orderBy);

        return query.First();
    }

    public virtual async Task<T> FirstAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    )
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate, include, orderBy);

        return await query.FirstAsync();
    }

    public virtual T FirstOrDefault(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    )
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate, include, orderBy);

        return query.FirstOrDefault();
    }

    public virtual async Task<T> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    )
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate, include, orderBy);

        return await query.FirstOrDefaultAsync();
    }
    #endregion

    #region Single
    public virtual T Single(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    )
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate, include);

        return query.Single();
    }



    public virtual async Task<T> SingleAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    )
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate, include);

        return await query.SingleAsync();
    }

    public virtual async Task<T> SingleAsNoTrackingAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    )
    {
        var query = _context.Set<T>().AsNoTracking().AsQueryable();

        query = PrepareQuery(query, predicate, include);

        return await query.SingleAsync();
    }

    public virtual T? SingleOrDefault(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    )
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate, include);

        return query.SingleOrDefault();
    }



    public virtual async Task<T?> SingleOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    )
    {
        var query = _context.Set<T>().AsQueryable();

        query = PrepareQuery(query, predicate, include);

        return await query.SingleOrDefaultAsync();
    }

    public virtual async Task<T?> SingleOrDefaultAsNoTrackingAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
    )
    {
        var query = _context.Set<T>().AsNoTracking().AsQueryable();

        query = PrepareQuery(query, predicate, include);

        return await query.SingleOrDefaultAsync();
    }
    #endregion

    #region Add
    public virtual void Add(T t)
    {
        _context.Add(t);
    }

    public virtual void Add(IEnumerable<T> t)
    {
        _context.AddRange(t);
    }

    public virtual async Task AddAsync(T t)
    {
        await _context.AddAsync(t);
    }

    public virtual async Task AddAsync(IEnumerable<T> t)
    {
        await _context.AddRangeAsync(t);
    }
    #endregion

    #region Remove
    public virtual void Remove(T t)
    {
        //if (t is IDeleted)
        //{
        //    ((IDeleted)t).IsDeleted = true;
        //    _context.Update(t);
        //}
        //else
        //{
        //    _context.Remove(t);
        //}

        _context.Remove(t);

    }

    public virtual void Remove(IEnumerable<T> t)
    {
        _context.RemoveRange(t);
    }

    public virtual async Task<int> ExecuteDeleteAsync(Expression<Func<T, bool>> predicate)
    {
        var eliminados = await _context.Set<T>().Where(predicate).ExecuteDeleteAsync();
        return eliminados;
    }

    public virtual int ExecuteDelete(Expression<Func<T, bool>> predicate)
    {
        var numberEliminated = _context.Set<T>().Where(predicate).ExecuteDelete();
        return numberEliminated;
    }
    #endregion

    #region Update
    public virtual void Update(T t)
    {
        _context.Update(t);
    }

    public virtual void Update(IEnumerable<T> t)
    {
        _context.UpdateRange(t);
    }

    public async Task<int> ExecuteUpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateFactory)
    {
        var modificados = await _context.Set<T>().Where(predicate).ExecuteUpdateAsync(updateFactory);

        return modificados;
    }

    public int ExecuteUpdate(Expression<Func<T, bool>> predicate, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateFactory)
    {
        var modificados = _context.Set<T>().Where(predicate).ExecuteUpdate(updateFactory);
        return modificados;
    }
    #endregion

    #region Queriable
    public virtual IQueryable<T> GetEntity(Expression<Func<T, bool>>? predicate = null)
    {
        //if (predicate != null)
        //{
        //    if (asNoTracking)
        //        return _context.Set<T>().Where(predicate).AsNoTracking();
        //    else
        //        return _context.Set<T>().Where(predicate).AsQueryable();
        //}
        //else
        //{
        //    if (asNoTracking)
        //        return _context.Set<T>().AsNoTracking();
        //    else
        //        return _context.Set<T>().AsQueryable();
        //}

        if (predicate != null)
        {
            return _context.Set<T>().Where(predicate).AsQueryable();

        }
        else
        {
            return _context.Set<T>().AsQueryable();
        }

    }

    //AsNoTracking

    public virtual IQueryable<T> GetEntityAsNoTracking(Expression<Func<T, bool>>? predicate = null)
    {
        if (predicate != null)
        {
            return _context.Set<T>().Where(predicate).AsNoTracking();
        }
        else
        {
            return _context.Set<T>().AsNoTracking();
        }

    }

    public virtual IQueryable<T> Query()
    {
        return _context.Set<T>();
    }

    public virtual IQueryable<T> QueryAsNoTracking()
    {
        return _context.Set<T>().AsNoTracking();
    }

    #endregion

}