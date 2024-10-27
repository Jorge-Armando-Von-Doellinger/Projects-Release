using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Entity.Base;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace HMS.Payments.Infrastructure.Connect
{
    public sealed class MongoContext
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        public MongoContext(/*IDatabaseSettings settings*/)
        {
            _client = new MongoClient(/*settings.ConnectionString*/);
            _database = _client.GetDatabase("Payment-MicroService");
            MapId();
        }

        void MapId()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(EntityBase))) return;
            BsonClassMap.RegisterClassMap<EntityBase>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapIdProperty(p => p.ID);
            });
        }

        internal IMongoCollection<PaymentEmployee> PaymentEmployee => _database.GetCollection<PaymentEmployee>("PaymentsEmployee");
        internal IMongoCollection<Payment> Payment => _database.GetCollection<Payment>("Payment");

        internal IMongoClient GetClient() => _client;
    }
}
