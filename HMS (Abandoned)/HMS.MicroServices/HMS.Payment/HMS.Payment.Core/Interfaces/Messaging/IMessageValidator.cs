namespace HMS.Payments.Core.Interfaces.Messaging;

public interface IMessageValidator
{
    Task ValidateMessageAsync(byte[] message);
}