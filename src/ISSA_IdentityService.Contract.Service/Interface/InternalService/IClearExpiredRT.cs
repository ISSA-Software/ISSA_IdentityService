namespace ISSA_IdentityService.Contract.Service.Interface.InternalService
{
    public interface IClearExpiredRT
    {
        Task ClearExpiredRTAsync(CancellationToken cancellationToken = default);
    }
}
