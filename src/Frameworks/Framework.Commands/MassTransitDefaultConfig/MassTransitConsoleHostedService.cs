using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class MassTransitConsoleHostedService :
    IHostedService
{
    readonly IBusControl _bus;

    public MassTransitConsoleHostedService(IBusControl bus, ILoggerFactory loggerFactory)
    {
        _bus = bus;
        
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _bus.StartAsync(cancellationToken).ConfigureAwait(false);
        
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return _bus.StopAsync(cancellationToken);
    }
}
// public class RegistrationStateDefinition :
//     SagaDefinition<OrderState>
// {
//     readonly IServiceProvider _provider;
//
//     public RegistrationStateDefinition(IServiceProvider provider)
//     {
//         _provider = provider;
//     }
//
//     protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator,
//         ISagaConfigurator<OrderState> consumerConfigurator)
//     {
//         endpointConfigurator.UseMessageRetry(r => r.Intervals(10, 50, 100, 1000, 1000, 1000, 1000, 1000));
//
//         endpointConfigurator.UseEntityFrameworkOutbox<OrderManagementContext>(_provider);
//     }
// }