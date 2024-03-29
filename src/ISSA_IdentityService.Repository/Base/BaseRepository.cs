using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ISSA_IdentityService.Contract.Repository.BaseInterface;
using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Core.Utils;

namespace ISSA_IdentityService.Repository.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        protected readonly DbContext _dbContext = null!;

        private DbSet<T> _dbSet;

        protected DbSet<T> DbSet
        {
            get
            {
                if (_dbSet != null)
                {
                    return _dbSet;
                }

                _dbSet = _dbContext.Set<T>();
                return _dbSet;
            }
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected BaseRepository(DbContext dbContext)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            var e = await DbSet.AddAsync(entity, cancellationToken);
            DbSet.Entry(e.Entity).State = EntityState.Added;
            _dbContext.SaveChanges();
            return e.Entity;
        }

        public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default, bool isHardDelete = false)
        {
            if(isHardDelete)
            {
                var k = await DbSet.Where(filter).ExecuteDeleteAsync(cancellationToken);
                return k;
            }
            var i = await DbSet.Where(filter).ExecuteUpdateAsync(x => x.SetProperty(x => x.IsDelete, true), cancellationToken: cancellationToken);
            return i;
        }

        public virtual async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[]? includes)
        {
            return await Task.Run(() =>
            {
                var query = DbSet.AsQueryable();
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                if (includes != null)
                {
                    query = includes.Aggregate(query, (current, include) => current.Include(include));
                }
                return query.AsNoTracking();
            }); ;
        }

        public virtual async Task<T?> GetSingleAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[]? includes)
        {
            var query = DbSet.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        ///  Update entity with specific properties
        /// </summary>
        /// <param name="filter"> Entity filter </param>
        /// <param name="update"> Lamda select per properties</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public virtual async Task<int> UpdateAsync(Expression<Func<T, bool>> filter, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> update, CancellationToken cancellationToken = default)
        {
            int i = await DbSet.Where(filter).ExecuteUpdateAsync(update, cancellationToken);
            await DbSet.Where(filter).ExecuteUpdateAsync(x => x.SetProperty(x => x.LastUpdatedTime, DateTime.UtcNow), cancellationToken);
            return i;
        }

        protected void TryAttach(T entity)
        {
            try
            {
                if (_dbContext.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        protected string? TryGetIdFromFilter(Expression<Func<T, bool>> filter)
        {
            try
            {
                if (filter.Body is BinaryExpression body)
                {
                    var ins = new ExpressionInspect(body);
                    return ins.Id;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Can not get id from filter", e);
            }

        }

        /// <summary>
        /// Update entity with all properties
        /// Change entity.Id property to null if you want to ignore that property
        /// </summary>
        /// <param name="t"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Affected rows</returns>
        public Task<int> UpdateAsync(T t, CancellationToken cancellationToken = default)
        {
            TryAttach(t);
            t.LastUpdatedTime = ObjHelper.ReplaceNullOrDefault(t.LastUpdatedTime, DateTime.UtcNow);
            _dbContext.Entry(t).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}