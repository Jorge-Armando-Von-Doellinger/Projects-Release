namespace HMS.ContractsMicroService.Infrastructure.Interfaces
{
    public interface ITransaction<Context>
    {
        Task Execute(Context context, Func<Task> func);
        //Task HasTransactionActive<Context>(Context context); 
    }
}
