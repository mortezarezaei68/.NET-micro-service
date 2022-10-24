using MassTransit;
using OrderManagement.Core.RequestCommand;

namespace OrderManagement.Core;

public class CreateOrderConsumer : IConsumer<CreateOrderConsumerRequest>
{
    public async Task Consume(ConsumeContext<CreateOrderConsumerRequest> context)
    {
        Console.WriteLine("consume create order");
    }
}