using Framework.Commands.CommandHandlers;

namespace ProductManagement.Core.HandlerCommands;

public class CreateProductDetailCommandRequest:RequestCommand
{
    public int ProductId { get; set; }
    public Dictionary<string,string> ProductDetails { get; set; }
}