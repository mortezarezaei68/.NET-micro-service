using MassTransit;
using OrderManagement.Core.Domain;
using OrderManagement.Core.RequestCommand;

namespace OrderManagement.Core;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public OrderStateMachine()
    {
        Event(() => OrderCreated, x => x.CorrelateById(m => m.Message.Id));
        Event(() => OrderUpdated, x => x.CorrelateById(m => m.Message.Id));

        InstanceState(x => x.State);

        Initially(
            When(OrderCreated)
                // .If(context => context.Saga.Id==0,
                //     fail => fail.Then(_ => throw new ApplicationException("Totally random, but you didn't pay enough for quality service")))
                .Then(context => UpdateSagaState(context.Saga, context.Message.Name))
                .TransitionTo(Updated)
                .Publish(context => new UpdateOrderConsumerRequest()
                {
                    Name = "test",
                    OrderId = "test1"
                }));
        
        //
        // During(Updated,
        //     When(OrderCreated)
        //         .TransitionTo(Updated),
        //     When(OrderUpdated).Then(x => UpdateSagaState(x.Saga, x.Message.Order)));

        //
        // During(Accepted,
        //     When(FulfillOrderFaulted)
        //         .Then(context => Console.WriteLine("Fulfill Order Faulted: {0}",
        //             context.Data.Exceptions.FirstOrDefault()?.Message))
        //         .TransitionTo(Faulted),
        //     When(FulfillmentFaulted)
        //         .TransitionTo(Faulted),
        //     When(FulfillmentCompleted)
        //         .TransitionTo(Completed));
        //
        // DuringAny(
        //     When(OrderStatusRequested)
        //         .RespondAsync(x => x.Init<OrderStatus>(new
        //         {
        //             OrderId = x.Instance.CorrelationId,
        //             State = x.Instance.CurrentState
        //         }))
        // );
        //
        //
        // DuringAny(
        //     When(OrderSubmitted)
        //         .Then(context =>
        //         {
        //             context.Instance.SubmitDate ??= context.Data.Timestamp;
        //             context.Instance.CustomerNumber ??= context.Data.CustomerNumber;
        //         })
        // );
    }

    private void UpdateSagaState(OrderState state, string orderName)
    {
        var currentDate = DateTime.Now;
        state.CreatedDate = currentDate;
        state.UpdatedDate = currentDate;
        state.EventId = Guid.NewGuid().ToString();
    }

    public State Created { get; private set; }
    public State Updated { get; private set; }

    public Event<UpdateOrderConsumerRequest> OrderUpdated { get; private set; }
    public Event<CreateOrderConsumerRequest> OrderCreated { get; private set; }
}