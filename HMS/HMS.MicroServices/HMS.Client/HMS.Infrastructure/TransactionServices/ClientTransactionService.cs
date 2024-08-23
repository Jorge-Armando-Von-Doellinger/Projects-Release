using HMS.Infrastructure.DataContext;

namespace HMS.Infrastructure.TransactionServices
{
    public class ClientTransactionService
    {
        internal async Task<int> ExecuteTransactionAsync(ClientContext context, Func<Task> action)
        {
            if(await HaveTransactionActive(context) == false)
            {
                Console.WriteLine("Transação ativa");
                return 0;
            }
            var transaction = context.Database.BeginTransaction();
            try
            {
                Console.WriteLine("1");

                await action();
                Console.WriteLine("2");

                int rowsAffected = await context.SaveChangesAsync();
                Console.WriteLine("3");
                return rowsAffected;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                await transaction.RollbackAsync();
                await transaction.DisposeAsync();
            }

        }

        internal async Task<bool> HaveTransactionActive(ClientContext context)
        {
            try
            {
                var transaction = context.Database.CurrentTransaction;
                Int16 count = 0;
                while(transaction != null || count < 500)
                {
                    await Task.Delay(10);
                    transaction = context.Database.CurrentTransaction;
                    if(transaction == null)
                        break;
                    count++;
                }
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
