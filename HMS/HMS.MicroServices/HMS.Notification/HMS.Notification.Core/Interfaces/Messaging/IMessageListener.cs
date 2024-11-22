namespace HMS.Notification.Core.Interfaces.Messaging;

public interface IMessageListener
{
    Task StartListeningAsync(CancellationToken cancellationToken);
}