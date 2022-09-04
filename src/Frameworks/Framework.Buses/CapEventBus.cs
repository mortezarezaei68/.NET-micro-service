using DotNetCore.CAP;

namespace Framework.Buses;

public class CapEventBus : ICapEventBus
{
    private readonly ICapPublisher _capPublisher;

    public CapEventBus(ICapPublisher capPublisher)
    {
        _capPublisher = capPublisher;
    }

    public Task Publish<TCommand>(TCommand command,string messageName, CancellationToken cancellationToken = default)
    {
        return _capPublisher.PublishAsync(messageName,command, cancellationToken: cancellationToken);
    }
}


