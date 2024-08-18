using HMS.Infrastructure.DataContext;

namespace HMS.Infrastructure.TransactionServices
{
    public class ClientTransactionService
    {
        internal async Task<int> ExecuteTransactionAsync(ClientContext context, Func<Task> action)
        {
            if(await HaveTransactionActive(context) == false)
                return 0;
            var transaction = context.Database.BeginTransaction();
            try
            {
                await action();
                int rowsAffected = await context.SaveChangesAsync();
                await transaction.CommitAsync();
                await transaction.DisposeAsync();
                return rowsAffected;
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                await transaction.DisposeAsync();
                throw ex;
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
