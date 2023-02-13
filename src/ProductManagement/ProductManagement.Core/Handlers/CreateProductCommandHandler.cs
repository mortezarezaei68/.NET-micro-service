using Confluent.Kafka;
using Framework.Commands.CommandHandlers;
using Framework.Domain.UnitOfWork;
using Framework.Exception.Exceptions.Enum;
using MassTransit;
using ProductManagement.Core.Domains;
using ProductManagement.Core.Domains.RepositoriesInterfaces;
using ProductManagement.Core.HandlerCommands;

namespace ProductManagement.Core.Handlers;


public class CreateProductCommandHandler:IConsumer<CreateProductCommandRequest>
{
    private readonly IProductRepository _repository;
    public CreateProductCommandHandler(IProductRepository repository) 
    {
        _repository = repository;
    }


    public async Task Consume(ConsumeContext<CreateProductCommandRequest> context)
    {
        await _repository.AddAsync(new Product(context.Message.Name),context.CancellationToken);
        
    }
}