using Framework.Commands.CommandHandlers;

namespace Framework.Buses;

public class CustomEventBus
{
    private readonly IServiceLocator _serviceLocator;

    public CustomEventBus(IServiceLocator serviceLocator)
    {
        _serviceLocator = serviceLocator;
    }

    public async Task Dispatch<T>(T command)
    {
        var handler = _serviceLocator.GetInstance<ICommandHandler<T>>();
        await handler.Handle(command);
    }
}