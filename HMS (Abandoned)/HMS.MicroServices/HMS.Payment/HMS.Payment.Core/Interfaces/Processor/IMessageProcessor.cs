using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Processor;

public interface IMessageProcessor
{
    Task ProcessMessageAsync(byte[] bytes);
    Task ProcessBatchMessageAsync(IEnumerable<byte[]> bytes);
    Task ReProcessMessageAsync(Message message);
}