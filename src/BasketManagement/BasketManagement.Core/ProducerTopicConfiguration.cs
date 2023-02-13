using Confluent.Kafka;
using Framework.Commands;
using MassTransit;

namespace BasketManagement.Core;

public class ProducerTopicConfiguration:TopicEndPoint<KafkaMessage>
{
    public ProducerTopicConfiguration(IRiderRegistrationContext context) : base(context)
    {
    }


    protected override void ActionMethod(IKafkaTopicReceiveEndpointConfigurator<Ignore, KafkaMessage> configurator)
    {
        throw new NotImplementedException();
    }
}