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


    public async Task Consume(ConsumeContext<CreateOrderConsumerRequest> context)
    {
        await context.Publish(new UpdateOrderConsumerRequest
        {
            Name = "test"
        });
    }
}

public class UpdateOrderConsumer : IConsumer<UpdateOrderConsumerRequest>
{
    public Task Consume(ConsumeContext<UpdateOrderConsumerRequest> context)
    {
        return Task.FromResult(new UpdateOrderConsumerResponse(true, ResultCode.Success));
    }
}