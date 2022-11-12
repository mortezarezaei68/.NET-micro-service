using MassTransit;
using OrderManagement.Core;

public class RegistrationStateDefinition :
    SagaDefinition<OrderState>
{
    readonly IServiceProvider _provider;

    public RegistrationStateDefinition(IServiceProvider provider)
    {
        _provider = provider;
    }

    protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator,
        ISagaConfigurator<OrderState> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(10, 50, 100, 1000, 1000, 1000, 1000, 1000));

        endpointConfigurator.UseEntityFrameworkOutbox<OrderManagementContext>(_provider);
    }
}