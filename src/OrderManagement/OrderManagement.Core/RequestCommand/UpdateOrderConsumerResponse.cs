using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace OrderManagement.Core.RequestCommand;

public class UpdateOrderConsumerResponse:ResponseCommand
{
    public UpdateOrderConsumerResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
    {
    }
}