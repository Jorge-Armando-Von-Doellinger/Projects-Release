using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Infrastructure.Context;
using HMS.ContractsMicroService.Infrastructure.Interfaces;
using HMS.ContractsMicroService.Infrastructure.Messages;
using HMS.ContractsMicroService.Infrastructure.Mongo.Utilities;
using MongoDB.Driver;
using SharpCompress.Common;
using System.ComponentModel;

namespace HMS.ContractsMicroService.Infrastructure.Repository
{
    public sealed class EmployeeContractRepository : IEmployeeContractRepository
    {
        private readonly IMongoCollection<EmployeeContract> _collection;
        private readonly ITransaction _transaction;

        public EmployeeContractRepository(MongoContext context, ITransaction transaction)
        {
            _collection = context.GetEmployeeContractsCollection();
            _transaction = transaction;
        }

        private async Task<bool> IdAlredyExist(string ID)
        {
            var doc = await _collection.FindAsync(doc => doc.ID == ID);
            return await doc.FirstOrDefaultAsync() != null;
        }

        public async Task AddAsync(EmployeeContract entity)
        {
            await _transaction.Execute(async (session) =>
            {
                await _collection.InsertOneAsync(session, entity);
            });
        }

        public async Task DeleteAsync(string entityId)
        {
            await _transaction.Execute(async (session) =>
            {
                await _collection.DeleteOneAsync(session, doc => doc.ID == entityId);
            });
        }

        public async Task<List<EmployeeContract>> GetAsync()
        {
            var contractsCursor = await _collection.FindAsync(P => true);
            return await contractsCursor.ToListAsync();
        }

        public async Task<EmployeeContract> GetByIdAsync(string entityId)
        {
            var data = await _collection.FindAsync(doc => doc.ID == entityId);
            return await data.FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException(MessageRecords.KeyNotFounded);
        }

        public async Task UpdateAsync(EmployeeContract entity)
        {
            var contract = await GetByIdAsync(entity.ID);
            contract.Update(entity);
            await _transaction.Execute(async (session) =>
            {
                await _collection.ReplaceOneAsync(session,
                    doc => doc.ID == entity.ID,
                    contract);
            });
        }


    }
}
