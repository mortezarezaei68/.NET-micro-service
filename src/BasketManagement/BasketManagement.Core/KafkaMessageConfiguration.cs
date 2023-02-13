using Confluent.Kafka;
using Framework.Commands;
using MassTransit;

namespace BasketManagement.Core;

public class KafkaMessageConfiguration:KafkaProducerConfiguration<KafkaMessage>
{
    public override void Configure(IRiderRegistrationContext context, IKafkaProducerConfigurator<Null, KafkaMessage> configurator)
    {
        Console.Write("test");
    }

}