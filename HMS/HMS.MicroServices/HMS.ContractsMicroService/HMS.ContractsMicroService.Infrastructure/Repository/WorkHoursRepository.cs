using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Infrastructure.Context;
using HMS.ContractsMicroService.Infrastructure.Interfaces;
using HMS.ContractsMicroService.Infrastructure.Messages;
using HMS.ContractsMicroService.Infrastructure.Mongo.Utilities;
using HMS.ContractsMicroService.Infrastructure.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

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
            _collection = context.GetWorkHoursCollection();
            _transaction = transaction;
        }
        public async Task AddAsync(WorkHours entity)
        {

            Console.WriteLine("Adicionado?");
            if (await IdAlredyExist(entity.ID))
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
                await _collection.DeleteOneAsync(session, MongoUtilities.WorkHoursFilterID(entityId));
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

        private async Task<bool> IdAlredyExist(string ID)
        {
            var doc = await _collection.FindAsync(x => x.ID == ID);
            return await doc.FirstOrDefaultAsync() != null;
        }

        public async Task<List<WorkHours>> GetAsync()
        {
            var data = await _collection.FindAsync(FilterDefinition<WorkHours>.Empty);
            return await data.ToListAsync();
        }

        public async Task<WorkHours> GetByIdAsync(string entityId)
        {
            var data = await _collection.FindAsync(MongoUtilities.WorkHoursFilterID(entityId));
            return await data.FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException(MessageRecords.KeyNotFounded);
        }

        public async Task UpdateAsync(WorkHours entity)
        {
            var workHours = await GetByIdAsync(entity.ID);
            workHours.Update(entity);
            await _transaction.Execute(_context.GetMongoClient(), async (session) =>
            {
                workHours.Update(entity);
                await _collection.ReplaceOneAsync(session, MongoUtilities.WorkHoursFilterID(entity.ID), workHours);
            });
        }
    }
}
