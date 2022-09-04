using DotNetCore.CAP;

namespace OrderManagement.Core;

public class SubscribeHandler:ISubscribeHandler
{
    [CapSubscribe("test")]
    public void CheckReceivedMessage(DateTime datetime)
    {
        throw new NotImplementedException();
    }
}

public interface ISubscribeHandler:ICapSubscribe
{
    void CheckReceivedMessage(DateTime datetime);
}