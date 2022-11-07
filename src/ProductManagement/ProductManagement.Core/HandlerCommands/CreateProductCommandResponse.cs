using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace ProductManagement.Core.HandlerCommands;

public class CreateProductCommandResponse:ResponseCommand
{
    public CreateProductCommandResponse(bool isSuccess, ResultCode resultCode, string message = null) : base(isSuccess, resultCode, message)
    {
    }
}