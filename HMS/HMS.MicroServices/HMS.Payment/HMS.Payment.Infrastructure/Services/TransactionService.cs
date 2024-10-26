using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace HMS.Payments.Infratructure.Services
{
    public sealed class TransactionService
    {
        private readonly IMongoClient _client;

        public TransactionService(IMongoClient client)
        {
            _client = client;
        }
        internal async Task Execute([NotNull] Func<IClientSessionHandle, Task> action)
        {
            // Sem using, pois o client é singleton!
            var session = await _client.StartSessionAsync();
            try
            {
                session.StartTransaction();
                await action(session);
                await session.CommitTransactionAsync();
                Console.WriteLine("Transaction closed successfull");
            }
            catch
            {
                await session.AbortTransactionAsync();
                Console.WriteLine("Error in the transaction");
                throw;
            }
        }
    }
}
