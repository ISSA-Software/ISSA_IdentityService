using System.Linq.Expressions;
using ISSA_IdentityService.Core.Models.Common;
using ISSA_IdentityService.Core.QueryObject;

namespace ISSA_IdentityService.Contract.Service.BaseServiceInterface;
public interface IGetAble<T, in TKey, Q> where T : class where Q : BaseQuery where TKey : notnull
{
    Task<ICollection<T>> GetAllAsync(Q query, CancellationToken cancellationToken = default);
    Task<PaginatedList<T>> GetPaginatedAsync(Q query, CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
}
