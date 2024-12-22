namespace HMS.Notification.Core.Interfaces.Messaging;

public interface IMessageProcessor
{
    Task Process(byte[] message);
    Task BatchProcess(List<byte[]> messages);
}