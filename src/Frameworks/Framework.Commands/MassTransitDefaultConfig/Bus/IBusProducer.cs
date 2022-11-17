namespace Contracts.Bus;

public interface IBusProducer
{
    Task ProduceAsync<T>(T message) where T : class;
}