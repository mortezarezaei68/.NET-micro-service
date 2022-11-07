using Framework.Commands.CommandHandlers;

namespace OrderManagement.Core.RequestCommand;

public class CreateOrderConsumerRequest:
    Framework.Commands.CommandHandlers.RequestCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}