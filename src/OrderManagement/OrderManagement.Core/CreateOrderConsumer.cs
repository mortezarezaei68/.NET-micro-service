using DotNetCore.CAP;
using Framework.Commands.CommandHandlers;
using Framework.Domain.UnitOfWork;
using Framework.Exception.Exceptions.Enum;
using MassTransit;
using MassTransit.Mediator;
using MassTransit.Transactions;
using OrderManagement.Core.RequestCommand;

namespace OrderManagement.Core;

public class CreateOrderConsumer : IConsumer<CreateOrderConsumerRequest>
{
    private readonly IMediator _transactionalBus;

    public CreateOrderConsumer(IMediator transactionalBus) 
    {
        _transactionalBus = transactionalBus;
    }

    public Task Consume(ConsumeContext<CreateOrderConsumerRequest> context)
    {
        _transactionalBus.Send(new UpdateOrderConsumerRequest
        {
            Name = "test"
        });
        return Task.FromResult(new CreateOrderConsumerResponse(true, ResultCode.Success));
    }
}

public class UpdateOrderConsumer : MassTransitTransactionalCommandHandler<UpdateOrderConsumerRequest,
    UpdateOrderConsumerResponse>
{
    public UpdateOrderConsumer(ITransactionalBus transactionalBus) : base(
        transactionalBus)
    {
    }

    public override Task<UpdateOrderConsumerResponse> Handle(UpdateOrderConsumerRequest command)
    {
        return Task.FromResult(new UpdateOrderConsumerResponse(true, ResultCode.Success));
    }
}