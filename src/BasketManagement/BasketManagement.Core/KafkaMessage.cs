using Framework.Commands.MassTransitDefaultConfig;
using Framework.Masstransit.KafkaIntegration;

namespace BasketManagement.Core;

public class KafkaMessage:IKafkaProducer
{
    public string Text { get; set; }
}