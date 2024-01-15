using ISSA_IdentityService.Contract.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ISSA_IdentityService.Service.BaseService;
public class BackgroundTaskConsumer(IServiceProvider service) : BackgroundService
{
    private IServiceProvider Services { get; } = service;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {

        using var scope = Services.CreateScope();
        var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IClearExpiredRT>();
        await scopedProcessingService.ClearExpiredRTAsync(stoppingToken);
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        await base.StopAsync(stoppingToken);
    }
}
