namespace HMS.Payments.Core.Data;

public sealed class MessageData
{
    public byte[] Message { get; set; }
    public ushort RetryCount { get; set; }
}