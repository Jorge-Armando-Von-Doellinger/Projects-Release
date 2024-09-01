using HMS.Employee.Infrastructure.MessageResponse;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace HMS.Employee.Infrastructure.Services
{
    public sealed class TransactionService
    {
        public TransactionService() { }

        public async Task<int> Execute(DbContext context, Func<Task> action)
        {
            await TransactionActive(context);
            var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await action();
                var rowsAffected = await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
                //return 0;
            }
            finally
            {
                await transaction.DisposeAsync();
                Console.WriteLine("Transação fechada");
            }
        }

        private async Task<bool> TransactionActive(DbContext context)
        {

            if(context.Database.CurrentTransaction == null)
            {
                return true;
            }
            var tcs = new TaskCompletionSource<bool>();
            await Task.Run(async () =>
            {
                for(var i = 0; i < 200; i++)
                {
                    if(context.Database.CurrentTransaction == null)
                    {
                        Console.WriteLine(i.ToString());
                        tcs.SetResult(true);
                        return;
                    }
                    await Task.Delay(10); //6x == 1s
                }
                tcs.SetResult(false);
            });
            bool result = await tcs.Task;
            if(result == false)
                throw new TimeoutException(DefaultMessages.ErrorTransactionActive);
            return result;
        }
    }
}
