namespace Framework.Commands.KafkaCommandHandlers;

public interface IKafkaCommandHandler<in TCommand>
{
    Task CommandHandler(TCommand command);
}
