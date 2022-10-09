using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace OrderManagement.Core.RequestCommand;

public class CreateOrderConsumerRequest:
    RequestCommandData
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class CreateOrderConsumerResponse:ResponseCommand
{
    public CreateOrderConsumerResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
    {
    }
}
public class UpdateOrderConsumerRequest:
    RequestCommandData
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string OrderId { get; set; }
}
public class UpdateOrderConsumerResponse:ResponseCommand
{
    public UpdateOrderConsumerResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
    {
    }
}
