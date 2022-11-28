using Confluent.Kafka;
using Dynamic.KafkaIntegration.Producer;
using MassTransit;

namespace BasketManagement.Core;

public class KafkaMessageConfiguration:IKafkaProducerConfiguration<KafkaMessage>
{
    public void Configure(IRiderRegistrationContext context, IKafkaProducerConfigurator<Null, KafkaMessage> configurator)
    {
        Console.Write("test");
    }

    public ProducerConfig ProducerConfig { get; set; }
}