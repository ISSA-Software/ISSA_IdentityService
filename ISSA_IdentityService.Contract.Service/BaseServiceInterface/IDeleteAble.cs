namespace ISSA_IdentityService.Contract.Service.BaseServiceInterface;
public interface IDeleteAble<in TKey> where TKey : class
{
    Task<int> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
}
