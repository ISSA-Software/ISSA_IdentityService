using System.Linq.Expressions;
using ISSA.Contract.Repository.Entity;
using Microsoft.EntityFrameworkCore.Query;

namespace ISSA.Contract.Repository.BaseInterface;
public interface IBaseRepository<T> where T : BaseEntity, new()
{
    Task<T?> GetSingleAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[]? includes);
    Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[]? includes);
    Task<int> UpdateAsync(Expression<Func<T, bool>> filter, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> update, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(T t, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
}
