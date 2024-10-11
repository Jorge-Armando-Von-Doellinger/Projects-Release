namespace HMS.ContractsMicroService.Application.Interfaces.Messaging
{
    internal interface IMessageHandler
    {
        Type DtoType { get; }
        Task HandleAsync(object input);
    }
}
