namespace HMS.Payments.Core.Interfaces.Processor
{
    public interface IMessageProcessor
    {
        Task Process(byte[] bytes);
        Task Process(List<byte[]> bytes);
    }
}
