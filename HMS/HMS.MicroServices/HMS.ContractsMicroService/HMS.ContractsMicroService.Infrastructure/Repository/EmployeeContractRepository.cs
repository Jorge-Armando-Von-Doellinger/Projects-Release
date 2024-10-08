﻿using HMS.ContractsMicroService.Core.Entity;
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
        private readonly MongoContext _context;
        private readonly IMongoCollection<EmployeeContract> _collection;
        private readonly ITransaction<IMongoClient> _transaction;

        public EmployeeContractRepository(MongoContext context, ITransaction<IMongoClient> transaction)
        {
            _context = context;
            _collection = context.GetEmployeeContractsCollection();
            _transaction = transaction;
        }

        private async Task<bool> IdAlredyExist(string ID)
        {
            var doc = await _collection.FindAsync(MongoUtilities.GetFilterID<EmployeeContract>(ID));
            return await doc.FirstOrDefaultAsync() != null;
        }

        public async Task AddAsync(EmployeeContract entity)
        {
            if(await IdAlredyExist(entity.ID))
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
            var contractsCursor = await _collection.FindAsync(P => true);
            return await contractsCursor.ToListAsync();
        }

        public async Task<EmployeeContract> GetByIdAsync(string entityId)
        {
            var data = await _collection.FindAsync(MongoUtilities.EmployeeContractFilterID(entityId));
            return await data.FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException(MessageRecords.KeyNotFounded);
        }

        public async Task UpdateAsync(EmployeeContract entity)
        {
            var contract = await GetByIdAsync(entity.ID);
            contract.Update(entity);
            await _transaction.Execute(_context.GetMongoClient(), async (session) =>
            {
                var filter = MongoUtilities.EmployeeContractFilterID(contract.ID);
                await _collection.ReplaceOneAsync(session, filter, contract);
            });
        }


    }
}
