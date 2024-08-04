using HMS.Infrastructure.DataContext;

namespace HMS.Infrastructure.TransactionServices
{
    public class ClientTransactionService
    {
        internal async Task<int> ExecuteTransactionAsync(ClientContext context, Func<Task> action)
        {
            using(context)
            using(var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    await action();
                    var rowsAffected = await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return rowsAffected;
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
