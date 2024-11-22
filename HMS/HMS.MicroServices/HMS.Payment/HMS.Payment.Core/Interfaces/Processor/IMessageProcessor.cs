using HMS.Payments.Core.Data;
using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Processor
{
    public interface IMessageProcessor
    {
        Task Process(byte[] bytes);
        Task Process(List<byte[]> bytes);
        Task ReProcess(Message message);
    }
}
