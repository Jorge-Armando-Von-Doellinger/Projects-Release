using HMS.ContractsMicroService.Core.Data;

namespace HMS.ContractsMicroService.Core.Interfaces.Messaging
{
    public interface IMessageListener
    {
        Task StartListener(Func<string, Task> action);
    }
}
