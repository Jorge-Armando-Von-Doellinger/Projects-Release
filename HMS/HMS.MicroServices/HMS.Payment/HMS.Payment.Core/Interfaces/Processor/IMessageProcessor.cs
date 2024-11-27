using HMS.Payments.Core.Data;
using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Processor
{
    public interface IMessageProcessor
    {
        Task BatchProcess(byte[] bytes);
        Task BatchProcess(List<byte[]> bytes);
        Task ReProcess(Message message);
    }
}
