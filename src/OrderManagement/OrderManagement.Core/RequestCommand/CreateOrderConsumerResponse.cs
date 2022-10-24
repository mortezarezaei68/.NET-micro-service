using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace OrderManagement.Core.RequestCommand;

public class CreateOrderConsumerResponse:ResponseCommand
{
    public CreateOrderConsumerResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
    {
    }
}