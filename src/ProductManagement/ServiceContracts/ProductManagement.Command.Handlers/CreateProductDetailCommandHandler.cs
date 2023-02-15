using Framework.Exception.Exceptions;
using Framework.Exception.Exceptions.Enum;
using MassTransit;
using ProductManagement.Commands;
using ProductManagement.Domains.RepositoriesInterfaces;

namespace ProductManagement.Command.Handlers;

public class CreateProductDetailCommandHandler : IConsumer<CreateProductDetailCommandRequest>
{
    private readonly IProductRepository _repository;

    public CreateProductDetailCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<CreateProductDetailCommandRequest> context)
    {
        var product = await _repository.GetByIdAsync(context.Message.ProductId, context.CancellationToken);
        if (product is null)
            throw new AppException("product not found", ResultCode.NotFound);
        
        product.UpdateProductDetail(context.Message.ProductDetails);
        _repository.Update(product);
    }
}