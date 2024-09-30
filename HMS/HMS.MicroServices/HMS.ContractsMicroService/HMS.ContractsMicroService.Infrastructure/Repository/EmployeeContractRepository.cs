using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Infrastructure.Context;
using HMS.ContractsMicroService.Infrastructure.Interfaces;
using HMS.ContractsMicroService.Infrastructure.Mongo.Utilities;
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
            if(await GetByIdAsync(entity.ID) != null)
                entity.RecreateID();
            await _transaction.Execute(_context.GetMongoClient(), async (session) =>
            {
                await _collection.InsertOneAsync(session, entity);
            });
        }

        public async Task DeleteAsync(string entityId)
        {
            await _transaction.Execute(_context.GetMongoClient(), async (session) =>
            {
                await _collection.DeleteOneAsync(session, MongoUtilities.EmployeeContractFilterID(entityId));
            });
        }

        public async Task<List<EmployeeContract>> GetAsync()
        {
            var contractsCursor = await _collection.FindAsync(FilterDefinition<EmployeeContract>.Empty);
            return await contractsCursor.ToListAsync();
        }

        public async Task<EmployeeContract> GetByIdAsync(string entityId)
        {

            var data = await _collection.FindAsync(MongoUtilities.EmployeeContractFilterID(entityId));
            return await data.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(EmployeeContract entity)
        {
            await _transaction.Execute(_context.GetMongoClient(), async (session) =>
            {
                var filter = MongoUtilities.EmployeeContractFilterID(entity.ID);
                await _collection.ReplaceOneAsync(session, filter, entity);
            });
        }


    }
}
