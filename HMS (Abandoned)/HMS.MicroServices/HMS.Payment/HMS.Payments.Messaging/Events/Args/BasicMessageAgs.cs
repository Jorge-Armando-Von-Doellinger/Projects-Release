namespace HMS.Payments.Messaging.Events.Args;

public class BasicMessageAgs : EventArgs
{
    public byte[] Message { get; set; }
    public ulong DeliveryTag { get; set; }
}