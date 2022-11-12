using Framework.Commands.MassTransitDefaultConfig;

namespace OrderManagement.Core;

public class OrderStateRegistration : RegistrationStateDefinition<OrderState, OrderManagementContext>
{
    public OrderStateRegistration(IServiceProvider provider) : base(provider)
    {
    }
}