using ISSA_IdentityService.Contract.Repository.Interface;
using ISSA_IdentityService.Contract.Service.Interface;
using Serilog;

namespace ISSA_IdentityService.Service.Services
{
    public class ClearExpiredRT(IRefreshTokenRepository repository, ILogger logger) : IClearExpiredRT
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
                        logger.Information("{result} refresh tokens cleared", result);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "ClearExpiredRTAsync: {ex}", ex.Message);
                }
                await Task.Delay(TimeSpan.FromHours(12), cancellationToken);
            }
        }
    }
}
 