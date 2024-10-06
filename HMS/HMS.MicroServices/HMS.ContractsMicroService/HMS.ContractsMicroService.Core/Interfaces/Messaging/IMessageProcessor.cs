namespace HMS.ContractsMicroService.Core.Interfaces.Messaging
{
    public interface IMessageProcessor
    {
        Task Process<T>(T message);
    }
}
