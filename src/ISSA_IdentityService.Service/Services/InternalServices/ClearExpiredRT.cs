using Invedia.DI.Attributes;
using ISSA_IdentityService.Contract.Repository.Interface;
using ISSA_IdentityService.Contract.Service.Interface;
using Microsoft.Extensions.Logging;

namespace ISSA_IdentityService.Service.Services.InternalServices
{
    [ScopedDependency(ServiceType = typeof(IClearExpiredRT))]
    public class ClearExpiredRT(IRefreshTokenRepository repository, ILogger<ClearExpiredRT> logger) : IClearExpiredRT
    {

        public async Task ClearExpiredRTAsync(CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = await repository.DeleteAsync(x => x.ExpiredAt < DateTime.Now, cancellationToken, true);
                    if (result > 0)
                    {
                        logger.LogInformation("{result} refresh tokens cleared", result);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "ClearExpiredRTAsync: {ex}", ex.Message);
                }
                await Task.Delay(TimeSpan.FromHours(12), cancellationToken);
            }
        }
    }
}
