using HMS.ContractsMicroService.Infrastructure.Interfaces;
using HMS.ContractsMicroService.Infrastructure.Messages;
using MongoDB.Driver;

namespace HMS.ContractsMicroService.Infrastructure.Services
{
    public sealed class TransactionService : ITransaction<IMongoClient>
    {
        public TransactionService()
        {

        }

        public async Task Execute(IMongoClient client, Func<IClientSessionHandle, Task> func)
        {
            var session = await client.StartSessionAsync();
            try
            {
                session.StartTransaction();
                await func(session);
                await session.CommitTransactionAsync();
            }
            catch
            {
                if (session.IsInTransaction)
                    await session.AbortTransactionAsync();
                throw;
            }
            finally
            {
                session.Dispose();
            }
        }

        /*internal async Task ExecuteSql(DbContext context, Func<Task> action)
        {
            await HasTransactionActiveSql(context);
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

        private async Task HasTransactionActiveSql(DbContext context)
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
        }*/
    }
}
