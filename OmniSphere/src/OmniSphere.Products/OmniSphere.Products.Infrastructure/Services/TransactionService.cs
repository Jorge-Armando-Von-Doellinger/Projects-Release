using System.Transactions;
using MongoDB.Driver;

namespace OmniSphere.Products.Infrastructure.Services;

public class TransactionService
{
    private readonly IMongoClient _client;

    public TransactionService(IMongoClient client)
    {
        _client = client;
    }

    public async Task ExecuteTransaction(Func<IClientSessionHandle, Task> action)
    {
        var session = await _client.StartSessionAsync();
        session.StartTransaction();
        try
        {
            await action(session);
            await session.CommitTransactionAsync();
        }
        catch
        {
            await session.AbortTransactionAsync();
            throw;
        }
        finally
        {
            session.Dispose();
        }
    }
}