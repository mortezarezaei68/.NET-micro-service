using MassTransit;
using OrderManagement.Core.Domain;

namespace OrderManagement.Core;

public class OrderState : SagaStateMachineInstance
{
    public int Id { get; set; }
    public Guid CorrelationId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string State { get; set; }
    public string EventId { get; set; }
}