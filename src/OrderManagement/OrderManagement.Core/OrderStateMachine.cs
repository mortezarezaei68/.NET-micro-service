using MassTransit;
using OrderManagement.Core.Domain;
using OrderManagement.Core.RequestCommand;

namespace OrderManagement.Core;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public OrderStateMachine()
    {
        Event(() => OrderCreated, x => 
            x.CorrelateById(m => m.Message.CorrelationId));
        Event(() => OrderUpdated, x => x.CorrelateById(m => m.Message.CorrelationId));

        InstanceState(x => x.State);

        Initially(
            When(OrderCreated)
                .Then(context => UpdateSagaState(context.Saga,context.Message.Order))
                .TransitionTo(Created));

        During(Updated,
            When(OrderCreated)
                .TransitionTo(Updated),
            When(OrderUpdated).Then(x => UpdateSagaState(x.Saga, x.Message.Order)));

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
        
    private void UpdateSagaState(OrderState state, Order order)
    {
        var currentDate = DateTime.Now;
        state.Created = currentDate;
        state.Updated = currentDate;
        state.Order = order;
    }

    public State Created { get; private set; }
    public State Updated { get; private set; }

    public Event<OrderCreated> OrderCreated { get; private set; }
    public Event<OrderUpdated> OrderUpdated { get; private set; }

}