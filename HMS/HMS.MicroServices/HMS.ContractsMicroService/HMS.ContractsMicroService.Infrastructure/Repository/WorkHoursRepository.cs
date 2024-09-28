﻿using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Infrastructure.Context;
using HMS.ContractsMicroService.Infrastructure.Interfaces;
using HMS.ContractsMicroService.Infrastructure.Messages;
using HMS.ContractsMicroService.Infrastructure.Mongo.Utilities;
using HMS.ContractsMicroService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace HMS.ContractsMicroService.Infrastructure.Repository
{
    public sealed class WorkHoursRepository : IWorkHoursRepository
    {
        private readonly MongoContext _context;
        private readonly ITransaction<IMongoClient> _transaction;
        private readonly IMongoCollection<WorkHours> _collection;

        public WorkHoursRepository(MongoContext context, ITransaction<IMongoClient> transaction)
        {
            _context = context;
            var a = context.GetEmployeeContractsCollection();
            _transaction = transaction;
        }
        public async Task AddAsync(WorkHours entity)
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
                await _collection.DeleteOneAsync(MongoUtilities.GetFilterID<WorkHours>(entityId));
            });
        }

        public async Task<WorkHours> FindWorkHours(WorkHours workHours)
        {

            var workHoursFiltered = await _context.WorkHours
                .Where(x => x.EntryTime == workHours.EntryTime &&
                                        x.IntervalStartTime == workHours.IntervalStartTime &&
                                        x.IntervalEndTime == workHours.IntervalEndTime &&
                                        x.ExitTime == workHours.ExitTime)
                .ToListAsync();
            return workHoursFiltered.FirstOrDefault();
        }

        public async Task<List<WorkHours>> GetAsync()
        {
            var data = await _collection.FindAsync(FilterDefinition<WorkHours>.Empty);
            return await data.ToListAsync();
        }

        public async Task<WorkHours> GetByIdAsync(string entityId)
        {
            var data = await _collection.FindAsync(MongoUtilities.GetFilterID<WorkHours>(entityId));
            return await data.FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException(MessageRecords.KeyNotFounded);
        }

        public async Task UpdateAsync(WorkHours entity)
        {
            var workHours = await GetByIdAsync(entity.ID);
            await _transaction.Execute(_context.GetMongoClient(), async () =>
            {
                workHours.Update(entity);
                await _collection.ReplaceOneAsync(MongoUtilities.GetFilterID<WorkHours>(entity.ID), workHours);
            });
        }
    }
}
