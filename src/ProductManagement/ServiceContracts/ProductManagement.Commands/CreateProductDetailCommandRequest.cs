using Framework.Commands.CommandHandlers;

namespace ProductManagement.Commands;

public class CreateProductDetailCommandRequest
{
    public int ProductId { get; set; }
    public Dictionary<string,string> ProductDetails { get; set; }
}