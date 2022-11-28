using Framework.Exception.Exceptions.Enum;
using MassTransit;
using OrderManagement.Core.RequestCommand;

namespace OrderManagement.Core;

public class UpdateOrderConsumer : IConsumer<UpdateOrderConsumerRequest>
{
    public Task Consume(ConsumeContext<UpdateOrderConsumerRequest> context)
    {
        return Task.FromResult(new UpdateOrderConsumerResult(true, ResultCode.Success));
    }
}