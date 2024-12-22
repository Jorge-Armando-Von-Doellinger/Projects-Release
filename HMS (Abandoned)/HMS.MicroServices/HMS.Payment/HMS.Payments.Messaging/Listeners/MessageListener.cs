using HMS.Payments.Core.Interfaces.Messaging;
using Microsoft.Extensions.Hosting;

namespace HMS.Payments.Messaging.Listeners;

public sealed class MessageListener : BackgroundService
{
    private readonly IEnumerable<IMessageListener> _listeners;

    public MessageListener(IEnumerable<IMessageListener> listeners)
    {
        _listeners = listeners;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Parallel.ForEachAsync(_listeners, stoppingToken,
            async (listener, cancellationToken) => { await listener.ListeningAsync(cancellationToken); });
    }
}