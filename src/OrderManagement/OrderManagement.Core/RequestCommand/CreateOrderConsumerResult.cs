using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace OrderManagement.Core.RequestCommand;

public class CreateOrderConsumerResult:ResultCommand
{
    public CreateOrderConsumerResult(bool isSuccess, ResultCode resultCode, string? message = null) : base(isSuccess, resultCode, message)
    {
    }
}