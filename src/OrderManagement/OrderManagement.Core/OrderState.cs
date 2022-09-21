using MassTransit;
using OrderManagement.Core.Domain;

namespace OrderManagement.Core;

public class OrderState : SagaStateMachineInstance
{
    public int OrderId { get; set; }
    public Guid CorrelationId { get; set; }
    public Order Order { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public string State { get; set; }
}