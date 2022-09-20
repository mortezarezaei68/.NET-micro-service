namespace Framework.Buses;

public interface ICustomEventBus
{
    Task Dispatch<T>(T command);
}