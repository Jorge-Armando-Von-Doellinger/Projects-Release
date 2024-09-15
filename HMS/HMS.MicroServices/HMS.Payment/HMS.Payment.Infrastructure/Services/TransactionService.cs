using HMS.Payments.Core.Entity;
using HMS.Payments.Infratructure.Context;
using HMS.Payments.Infratructure.Messages.Errors;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace HMS.Payments.Infratructure.Services
{
    public sealed class TransactionService
    {
        internal async Task Execute([NotNull]DbContext context, [NotNull] Func<Task> action)
        {
            await HasTransactionAtive(context);
            var transaction = context.Database.BeginTransaction();
            try
            {
                await action();
                var rowsAffected = await context.SaveChangesAsync();
                if (rowsAffected != 1) throw new Exception(DefaultErrorMessages.ErrorInTransaction);
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

        private async Task<bool> HasTransactionAtive(DbContext context)
        {
            if(context.Database.CurrentTransaction == null) return true;
            var tcp = new TaskCompletionSource<bool>();
            await Task.Run(async () =>
            {
                for (var i = 0; i< 200; i++)
                {
                    if(context.Database.CurrentTransaction == null)
                    {
                        tcp.SetResult(true);
                        return;
                    }
                    await Task.Delay(10); // 6x = 1s, logo 200x = 33s+-
                }
            });
            return await tcp.Task ? true 
                : throw new TimeoutException(DefaultErrorMessages.TimeOutInTransaction);
        }
    }
}
