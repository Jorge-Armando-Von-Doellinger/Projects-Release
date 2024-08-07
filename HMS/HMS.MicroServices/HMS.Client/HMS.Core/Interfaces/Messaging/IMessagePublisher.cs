namespace HMS.Core.Interfaces.Messaging
{
    public interface IMessagePublisher
    {
        Task<bool> PublishMessage(object data);
    }
}
