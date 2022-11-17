using Confluent.Kafka;
using MassTransit;

namespace Framework.Commands.MassTransitDefaultConfig;

public interface IKafkaProducer<T>
{
    public string TopicName { get; set; }
    public ProducerConfig ProducerConfig { get; set; }
    public Action<IRiderRegistrationContext, IKafkaProducerConfigurator<Null, T>> configure { get; set; }
}