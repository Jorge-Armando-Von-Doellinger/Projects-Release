using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using ZstdSharp.Unsafe;

namespace HMS.ContractsMicroService.Infrastructure.Repository
{
    public sealed class WorkHoursRepository : IWorkHoursRepository
    {
        private readonly WorkHoursContext _context;

        public WorkHoursRepository(WorkHoursContext context)
        {
            _context = context;
        }
        public Task AddAsync(WorkHours entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid entityId)
        {
            throw new NotImplementedException();
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

        public Task<List<WorkHours>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WorkHours> GetByIdAsync(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(WorkHours entity)
        {
            throw new NotImplementedException();
        }
    }
}
