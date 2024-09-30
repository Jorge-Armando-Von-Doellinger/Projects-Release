using MongoDB.Driver;

namespace HMS.ContractsMicroService.Infrastructure.Interfaces
{
    public interface ITransaction<Context>
    {
        Task Execute(Context context, Func<IClientSessionHandle, Task> func);
        //Task HasTransactionActive<Context>(Context context); 
    }
}
