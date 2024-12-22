using HMS.Payments.Messaging.Events.Args;

namespace HMS.Payments.Messaging.Events;

internal delegate Task OnMessageReceived(object? sender, BasicMessageAgs args);

public sealed class MessageEvents
{
    internal event OnMessageReceived OnMessageReceived;
}