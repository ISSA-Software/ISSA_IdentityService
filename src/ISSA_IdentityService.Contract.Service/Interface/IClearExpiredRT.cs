namespace ISSA_IdentityService.Contract.Service.Interface
{
    public interface IClearExpiredRT
    {
        Task ClearExpiredRTAsync(CancellationToken cancellationToken = default);
    }
}
