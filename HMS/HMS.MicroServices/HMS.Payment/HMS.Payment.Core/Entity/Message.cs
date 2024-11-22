namespace HMS.Payments.Core.Entity;

public sealed class Message
{
    public byte[] Content { get; set; }
    public short Attempts { get; set; }
}