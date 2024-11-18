using HMS.Payments.Core.Data;

namespace HMS.Payments.Core.Interfaces.Processor
{
    public interface IMessageProcessor
    {
        Task TryProcess(byte[] bytes);
        Task<(bool success, List<MessageData>? MessageWithErrors)> TryProcess(List<byte[]> bytes);
    }
}
