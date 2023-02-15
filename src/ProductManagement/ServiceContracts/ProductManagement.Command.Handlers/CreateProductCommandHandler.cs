using MassTransit;
using ProductManagement.Commands;
using ProductManagement.Domains;
using ProductManagement.Domains.Repositories;

namespace ProductManagement.Command.Handlers;


public class CreateProductCommandHandler:IConsumer<CreateProductCommandRequest>
{
    private readonly IProductRepository _repository;
    public CreateProductCommandHandler(IProductRepository repository) 
    {
        _repository = repository;
    }
    
    public Task Consume(ConsumeContext<CreateProductCommandRequest> context)
    {
         _repository.Add(new Product(context.Message.Name));
         return Task.CompletedTask;
    }
}