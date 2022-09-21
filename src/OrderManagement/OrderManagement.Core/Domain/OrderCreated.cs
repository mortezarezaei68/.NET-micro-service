namespace OrderManagement.Core.Domain;

public class OrderCreated
{
    public Guid CorrelationId { get; set; }
    public Order Order { get; set; }   
}

public class OrderUpdated
{
    public Guid CorrelationId { get; set; }
    public Order Order { get; set; }
}