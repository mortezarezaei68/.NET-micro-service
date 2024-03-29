using Framework.Commands.CommandHandlers;
using Framework.Exception.Exceptions.Enum;

namespace ProductManagement.Commands;

public class CreateProductCommandResult:ResultCommand
{
    public CreateProductCommandResult(bool isSuccess, ResultCode resultCode, string? message = null) : base(isSuccess, resultCode, message)
    {
    }
}