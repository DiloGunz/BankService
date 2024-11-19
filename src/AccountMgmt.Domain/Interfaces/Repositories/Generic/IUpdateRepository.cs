using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AccountMgmt.Domain.Interfaces.Repositories.Generic;

public interface IUpdateRepository<T> where T : class
{
    void Update(T t);
    void Update(IEnumerable<T> t);

    Task<int> ExecuteUpdateAsync(Expression<Func<T, bool>> predicate, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateFactory);
    int ExecuteUpdate(Expression<Func<T, bool>> predicate, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateFactory);
}