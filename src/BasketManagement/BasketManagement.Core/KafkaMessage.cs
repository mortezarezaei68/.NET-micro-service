using Framework.Commands;

namespace BasketManagement.Core;

public class KafkaMessage:IKafkaProducer
{
    public string Text { get; set; }
}