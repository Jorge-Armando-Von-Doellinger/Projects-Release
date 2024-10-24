﻿using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Infrastructure.Context;
using HMS.ContractsMicroService.Infrastructure.Interfaces;
using HMS.ContractsMicroService.Infrastructure.Messages;
using MongoDB.Driver;

namespace HMS.ContractsMicroService.Infrastructure.Repository
{
    public sealed class WorkHoursRepository : IWorkHoursRepository
    {
        private readonly MongoContext _context;
        private readonly ITransaction _transaction;
        private readonly IMongoCollection<WorkHours> _collection;

        public WorkHoursRepository(MongoContext context, ITransaction transaction)
        {
            _context = context;
            _collection = context.GetWorkHoursCollection();
            _transaction = transaction;
        }
        public async Task AddAsync(WorkHours entity)
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

        public async Task<WorkHours> FindWorkHours(WorkHours workHours)
        {
            var data = await _collection.FindAsync(x => x.EntryTime == workHours.EntryTime &&
                x.ExitTime == workHours.ExitTime &&
                x.IntervalStartTime == workHours.IntervalStartTime &&
                x.IntervalEndTime == workHours.IntervalEndTime);
            return await data.FirstOrDefaultAsync();
        }

        public async Task<List<WorkHours>> GetAsync()
        {
            var data = await _collection.FindAsync(doc => true);
            return await data.ToListAsync();
        }

        public async Task<WorkHours> GetByIdAsync(string entityId)
        {
            var data = await _collection.FindAsync(doc => doc.ID == entityId);
            return await data.FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException(MessageRecords.KeyNotFounded);
        }

        public async Task UpdateAsync(WorkHours entity)
        {
            var workHours = await GetByIdAsync(entity.ID);
            workHours.Update(entity);
            await _transaction.Execute(async (session) =>
            {
                workHours.Update(entity);
                await _collection.ReplaceOneAsync(session,
                    doc => doc.ID == entity.ID,
                    workHours);
            });
        }
    }
}
