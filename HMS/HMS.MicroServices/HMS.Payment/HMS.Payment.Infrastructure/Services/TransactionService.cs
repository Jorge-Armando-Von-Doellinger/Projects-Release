using HMS.Payment.Infrastructure.Connect;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace HMS.Payments.Infratructure.Services
{
    public sealed class TransactionService
    {
        internal async Task Execute([NotNull]IMongoClient client, [NotNull] Func<IClientSession, Task> action)
        {
            // Sem using, pois o client é singleton!
            var session = await client.StartSessionAsync();
            session.StartTransaction();
            try
            {
                await action(session);
                await session.CommitTransactionAsync();
                Console.WriteLine("Transaction closed successfull");
            }
            catch
            {
                await session.AbortTransactionAsync();
                Console.WriteLine("Error in the transaction");
            }
        }
    }
}
