using System.Diagnostics.CodeAnalysis;
using MongoDB.Driver;

namespace HMS.Payments.Infrastructure.Services;

public sealed class TransactionService
{
    private readonly IMongoClient _client;
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
            }
        }
    }
}