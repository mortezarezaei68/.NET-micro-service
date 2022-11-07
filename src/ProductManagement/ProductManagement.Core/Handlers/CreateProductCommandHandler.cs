using Framework.Commands.CommandHandlers;
using Framework.Domain.UnitOfWork;
using Framework.Exception.Exceptions.Enum;
using ProductManagement.Core.Domains;
using ProductManagement.Core.Domains.RepositoriesInterfaces;
using ProductManagement.Core.HandlerCommands;

namespace ProductManagement.Core.Handlers;


public class CreateProductCommandHandler:MassTransitTransactionalCommandHandler<CreateProductCommandRequest,CreateProductCommandResponse>
{
    private readonly IProductRepository _repository;
    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository repository) : base(unitOfWork)
    {
        _repository = repository;
    }
    protected override async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest command,CancellationToken cancellationToken)
    {
        await _repository.AddAsync(new Product(command.Name), cancellationToken);
        return new CreateProductCommandResponse(true,ResultCode.Success);
    }


}