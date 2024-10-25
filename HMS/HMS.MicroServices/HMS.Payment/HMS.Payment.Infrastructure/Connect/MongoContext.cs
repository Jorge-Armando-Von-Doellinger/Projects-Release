using HMS.Payments.Core.Entity;
using MongoDB.Driver;

namespace HMS.Payment.Infrastructure.Connect
{
    public sealed class MongoContext
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        public MongoContext(IMongoClient client)
        {
            _client = client;
            _database = _client.GetDatabase("Payment-MicroService");
        }

        internal IMongoCollection<EmployeePayment> Employee => _database.GetCollection<EmployeePayment>("Employee");

        internal IMongoClient GetClient() => _client;
    }
}
