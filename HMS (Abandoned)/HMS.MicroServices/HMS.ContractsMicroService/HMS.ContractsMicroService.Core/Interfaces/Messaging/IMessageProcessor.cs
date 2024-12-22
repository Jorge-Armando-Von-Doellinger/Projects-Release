using HMS.ContractsMicroService.Core.Data;

namespace HMS.ContractsMicroService.Core.Interfaces.Messaging
{
    public interface IMessageProcessor
    {
        Task Process(string data);
    }
}
