using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Infrastructure.Context;
using HMS.ContractsMicroService.Infrastructure.Interfaces;
using HMS.ContractsMicroService.Infrastructure.Messages;
using HMS.ContractsMicroService.Infrastructure.Mongo.Utilities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace HMS.ContractsMicroService.Infrastructure.Repository
{
    public sealed class EmployeeContractRepository : IEmployeeContractRepository
    {
        private readonly MongoContext _context;
        private readonly IMongoCollection<EmployeeContract> _collection;
        private readonly ITransaction<IMongoClient> _transaction;

        public EmployeeContractRepository(MongoContext context, ITransaction<IMongoClient> transaction)
        {
            _context = context;
            _collection = context.GetEmployeeContractsCollection();
            _transaction = transaction;
        }

        public async Task AddAsync(EmployeeContract entity)
        {
            await _transaction.Execute(_context.GetMongoClient(), async () =>
            {
                await _collection.InsertOneAsync(entity);
            });
        }

        public async Task DeleteAsync(string entityId)
        {
            await _transaction.Execute(_context.GetMongoClient(), async () =>
            {
                //var filter = new FilterDefinition<EmployeeContract>().;
                //var contract = await _collection.DeleteOne()
            });
        }

        public async Task<List<EmployeeContract>> GetAsync()
        {
            var contractsCursor = await _collection.FindAsync(FilterDefinition<EmployeeContract>.Empty);
            return await contractsCursor.ToListAsync();
        }

        public async Task<EmployeeContract> GetByIdAsync(string entityId)
        {

            var data = await _collection.FindAsync(MongoUtilities.GetFilterID<EmployeeContract>(entityId));
            return await data.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(EmployeeContract entity)
        {
            await _transaction.Execute(_context.GetMongoClient(), async () =>
            {
                var filter = MongoUtilities.GetFilterID<EmployeeContract>(entity.ID);
                var a = _collection.ReplaceOneAsync(filter, entity);
            });
        }


    }
}
