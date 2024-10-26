using HMS.Payments.Infrastructure.Settings.Interfaces;
using HMS.Payments.Core.Entity;
using MongoDB.Driver;

namespace HMS.Payments.Infrastructure.Connect
{
    public sealed class MongoContext
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        public MongoContext(IDatabaseSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase("Payment-MicroService");
        }

        internal IMongoCollection<EmployeePayment> Employee => _database.GetCollection<EmployeePayment>("Employee");

        internal IMongoClient GetClient() => _client;
    }
}
