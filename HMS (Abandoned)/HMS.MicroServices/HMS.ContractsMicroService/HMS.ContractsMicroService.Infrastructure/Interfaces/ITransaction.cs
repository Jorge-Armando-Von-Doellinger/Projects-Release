using MongoDB.Driver;

namespace HMS.ContractsMicroService.Infrastructure.Interfaces
{
    public interface ITransaction
    {
        Task Execute(Func<IClientSessionHandle, Task> func);
        //Task HasTransactionActive<Context>(Context context); 
    }
}
