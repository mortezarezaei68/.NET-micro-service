namespace Framework.Buses;

public interface ICapEventBus
{
    Task Publish<TCommand>(TCommand command,string messageName, CancellationToken cancellationToken = default);
}