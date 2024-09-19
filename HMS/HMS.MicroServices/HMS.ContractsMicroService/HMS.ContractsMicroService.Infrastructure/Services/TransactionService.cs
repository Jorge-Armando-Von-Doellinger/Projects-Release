using HMS.ContractsMicroService.Infrastructure.Messages;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace HMS.ContractsMicroService.Infrastructure.Services
{
    public sealed class TransactionService
    {
        public TransactionService()
        {

        }

        internal async Task Execute(DbContext context, Func<Task> action)
        {
            await HasTransactionActive(context);
            var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await action();
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        private async Task HasTransactionActive(DbContext context)
        {
            var transactionTask = Task.Run(async () =>
            {
                while (context.Database.CurrentTransaction != null)
                {
                    await Task.Delay(10);
                }
            });
            var timeoutTask = Task.Delay(3000);
            if (await Task.WhenAny(transactionTask, timeoutTask) == timeoutTask)
                throw new TimeoutException(MessageRecords.Timeout);
        }
    }
}
