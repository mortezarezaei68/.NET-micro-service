using Confluent.Kafka;
using MassTransit;

namespace Framework.Commands.MassTransitDefaultConfig;

public abstract class TopicEndPoint
    <TProducer> where TProducer : class, IProducer, new()
{
    private readonly IRiderRegistrationContext _context;
    protected TopicEndPoint(IRiderRegistrationContext context)
    {
        _context = context;
    }
    public string GroupId { get; }

    public string? TopicName => typeof(TProducer).Name;
    public abstract void ActionMethod(IKafkaTopicReceiveEndpointConfigurator<Ignore, TProducer> configurator);
}