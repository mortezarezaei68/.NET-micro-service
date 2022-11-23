using Confluent.Kafka;
using Framework.Commands.MassTransitDefaultConfig;
using Framework.Masstransit.KafkaIntegration;
using MassTransit;
using SharedLibrary.Core.Messages;

namespace BasketManagement.Core;

public class KafkaMessageConfiguration:IKafkaProducerConfiguration<KafkaMessage>
{
    public void Configure(IRiderRegistrationContext context, IKafkaProducerConfigurator<Null, KafkaMessage> configurator)
    {
        Console.Write("test");
    }

    public ProducerConfig ProducerConfig { get; set; }
}