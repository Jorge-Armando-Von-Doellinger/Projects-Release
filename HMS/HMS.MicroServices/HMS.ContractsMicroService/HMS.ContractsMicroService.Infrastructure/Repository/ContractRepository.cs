using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Infrastructure.Context;
using HMS.ContractsMicroService.Infrastructure.Interfaces;
using HMS.ContractsMicroService.Infrastructure.Messages;
using MongoDB.Driver;

namespace HMS.ContractsMicroService.Infrastructure.Repository
{
    public class ContractRepository : IContractRepository
    {
        private readonly IMongoCollection<Contract> _collection;
        private readonly ITransaction _transaction;

        public ContractRepository(MongoContext context, ITransaction transaction)
        {
            _collection = context.GetGeneralContractCollection();
            _transaction = transaction;
        }
        public async Task AddAsync(Contract entity)
        {
            await _transaction.Execute(async (session) =>
            {
                await _collection.InsertOneAsync(entity);
            });
        }

        public async Task DeleteAsync(string entityId)
        {
            await _transaction.Execute(async (session) =>
            {
                await _collection.DeleteOneAsync(session, doc => doc.ID == entityId);
            });
        }

        public async Task<List<Contract>> GetAsync()
        {
            var contracts = await _collection.FindAsync(doc => true);
            return await contracts.ToListAsync();
        }

        public async Task<Contract> GetByIdAsync(string entityId)
        {
            var contract = await _collection.FindAsync(doc => doc.ID == entityId);
            return await contract.FirstOrDefaultAsync() ?? throw new KeyNotFoundException(MessageRecords.KeyNotFounded); 
        }

        public async Task UpdateAsync(Contract entity)
        {
            await _transaction.Execute(async (session) =>
            {
                await _collection.ReplaceOneAsync(session,
                    doc => doc.ID == entity.ID,
                    entity);
            });
        }
    }
}
