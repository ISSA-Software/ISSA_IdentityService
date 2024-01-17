using ISSA_IdentityService.Contract.Service.Interface.InternalService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace ISSA_IdentityService.Service.Services.InternalServices;
public class BackgroundTaskConsumer(IServiceProvider service) : BackgroundService
{
    private IServiceProvider Services { get; } = service;

    protected override async Task ExecuteAsync( CancellationToken cancellationToken )
    {
        await DoWork( cancellationToken );
    }

    private async Task DoWork(CancellationToken cancellationToken)
    {
        using var scope = Services.CreateScope();
        var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IClearExpiredRT>();
        await scopedProcessingService.ClearExpiredRTAsync(cancellationToken);
    }

    public override async Task StopAsync( CancellationToken cancellationToken )
    {
        await base.StopAsync( cancellationToken );
    }
}
