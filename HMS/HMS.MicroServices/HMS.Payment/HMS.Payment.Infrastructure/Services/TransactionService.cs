using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace HMS.Payments.Infratructure.Services
{
    public sealed class TransactionService
    {
        private readonly IMongoClient _client;
        private static short _max = 50;
        private static short activeNow = 0;
        public TransactionService(IMongoClient client)
        {
            _client = client;
        }
        internal async Task ExecuteAsync([NotNull] Func<IClientSessionHandle, Task> action)
        {
            using (var session = await _client.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();
                    await action(session);
                    await session.CommitTransactionAsync();
                    //Console.WriteLine("Transaction closed successfull");
                }
                catch (Exception ex)
                {
                    await session.AbortTransactionAsync();
                    Console.WriteLine("Error in the transaction");
                    Console.WriteLine("--> " + ex.Message);
                    throw;
                }
            }
            activeNow--;
        }
    }
}
