using HMS.Payments.Core.Interfaces.Messaging;

namespace HMS.Payments.Grpc.App_Services.Background_Services;

public class MessageListenerStart : BackgroundService
{
    private readonly IMessageListener _listener;

    public MessageListenerStart(IMessageListener listener)
    {
        _listener = listener;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _listener.ListeningAsync(stoppingToken);
    }
}