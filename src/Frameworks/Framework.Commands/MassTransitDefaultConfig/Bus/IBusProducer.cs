namespace Framework.Commands.MassTransitDefaultConfig.Bus;

public interface IBusProducer
{
    Task ProduceAsync<T>(T message) where T : class;
}