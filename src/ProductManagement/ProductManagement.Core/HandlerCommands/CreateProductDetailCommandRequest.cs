using Framework.Commands.CommandHandlers;

namespace ProductManagement.Core.HandlerCommands;

public class CreateProductDetailCommandRequest:RequestCommand
{
    public string Key { get; init; }
    public string Value { get; init; }
}