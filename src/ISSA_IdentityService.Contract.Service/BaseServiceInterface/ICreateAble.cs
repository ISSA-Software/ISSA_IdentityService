namespace ISSA_IdentityService.Contract.Service.BaseServiceInterface
{
    public interface ICreateAble<in T, TKey> where T : class, new()
    {
        Task<TKey> CreateAsync(T model, CancellationToken cancellationToken = default);
    }
}
