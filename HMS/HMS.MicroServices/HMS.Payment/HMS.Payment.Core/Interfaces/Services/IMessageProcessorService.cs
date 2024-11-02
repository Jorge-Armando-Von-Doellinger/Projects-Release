namespace HMS.Payments.Core.Interfaces.Services
{
    public interface IMessageProcessorService
    {
        Task Process(byte[] bytes);
    }
}
