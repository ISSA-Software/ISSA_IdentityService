namespace ISSA_IdentityService.Contract.Service.BaseServiceInterface;
public interface IUpdateAble<in T, in Tkey> where T : class, new()
{ 
    Task<int> UpdateAsync(Tkey id, T model, CancellationToken cancellationToken = default);
}
