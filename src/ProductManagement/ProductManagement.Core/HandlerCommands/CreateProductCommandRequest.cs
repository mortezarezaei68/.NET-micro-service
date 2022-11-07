using Framework.Commands.CommandHandlers;

namespace ProductManagement.Core.HandlerCommands;

public class CreateProductCommandRequest:RequestCommand
{
    public string? Name { get; init; }
}