using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Core.Models.Common;

namespace ISSA_IdentityService.Contract.Repository.BaseInterface;
public interface IBaseCacheLayer<T> where T : BaseEntity, new()
{
    Task<T?> GetSingleAsync(string key, CancellationToken cancellationToken = default);
    Task AddSingleAsync(T entity, CancellationToken cancellationToken = default);
    Task RefreshKeyAsync(string key, CancellationToken cancellationToken = default);
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    Task<string[]> GetAvailableKey(CancellationToken cancellationToken = default);
    Task AddMultipleAsync(string key, PaginatedList<T> entities, CancellationToken cancellationToken = default);
    Task<PaginatedList<T>?> GetMultipleAsync(string keys, CancellationToken cancellationToken = default);
    Task RemoveMultipleAsync(string[] keyPrefix, CancellationToken cancellationToken = default);
}
