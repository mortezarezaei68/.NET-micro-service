using Framework.Commands.CommandHandlers;

namespace OrderManagement.Core.RequestCommand;

public class UpdateOrderConsumerRequest:
    Framework.Commands.CommandHandlers.RequestCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string OrderId { get; set; }
}