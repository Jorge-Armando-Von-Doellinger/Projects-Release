using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Infrastructure.Context;
using HMS.ContractsMicroService.Infrastructure.Messages;
using HMS.ContractsMicroService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace HMS.ContractsMicroService.Infrastructure.Repository
{
    public sealed class WorkHoursRepository : IWorkHoursRepository
    {
        private readonly WorkHoursContext _context;
        private readonly TransactionService _transaction;

        public WorkHoursRepository(WorkHoursContext context, TransactionService transaction)
        {
            _context = context;
            _transaction = transaction;
        }
        public async Task AddAsync(WorkHours entity)
        {
            await _transaction.Execute(_context, async () =>
            {
                await _context.WorkHours.AddAsync(entity);
            });
        }

        public async Task DeleteAsync(Guid entityId)
        {
            await _transaction.Execute(_context, async () =>
            {
                var workHours = await this.GetByIdAsync(entityId);
                await Task.Run(() => _context.WorkHours.Remove(workHours));
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
            return await _context.WorkHours
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<WorkHours> GetByIdAsync(Guid entityId)
        {
            return await _context.WorkHours.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID == entityId)
                    ?? throw new KeyNotFoundException(MessageRecords.KeyNotFounded);
        }

        public async Task UpdateAsync(WorkHours entity)
        {
            await _transaction.Execute(_context, async () =>
            {
                var workHours = await this.GetByIdAsync(entity.ID);
                workHours.Update(entity);
                _context.WorkHours.Update(workHours);
            });
        }
    }
}
