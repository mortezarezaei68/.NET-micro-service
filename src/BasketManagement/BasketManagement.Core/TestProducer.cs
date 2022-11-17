using Confluent.Kafka;
using Framework.Commands.MassTransitDefaultConfig;
using MassTransit;

namespace BasketManagement.Core;

public class TestProducer : TopicEndPoint<Test>
{
    private readonly IRiderRegistrationContext _context;
    public TestProducer(IRiderRegistrationContext context) : base(context)
    {
        _context = context;
    }
    public string GroupId => "GroupTest";
    public override void ActionMethod(IKafkaTopicReceiveEndpointConfigurator<Ignore, Test> configurator)
    {
        configurator.ConfigureConsumers(_context);
        configurator.AutoOffsetReset = AutoOffsetReset.Earliest;
        configurator.CreateIfMissing(t =>
        {
            t.NumPartitions = 1; //number of partitions
            t.ReplicationFactor = 1; //number of replicas
        });
    }


}