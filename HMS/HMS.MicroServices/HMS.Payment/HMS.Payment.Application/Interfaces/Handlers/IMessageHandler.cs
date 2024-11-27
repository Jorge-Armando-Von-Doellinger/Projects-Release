namespace HMS.Payments.Application.Interfaces.Handlers;

public interface IMessageHandler
{
    internal Task HandleAsync(string json);
}