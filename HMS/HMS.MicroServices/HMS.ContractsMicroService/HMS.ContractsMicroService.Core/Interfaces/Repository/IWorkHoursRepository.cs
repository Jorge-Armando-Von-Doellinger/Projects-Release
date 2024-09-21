using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Interfaces.Repository.BaseRepository;

namespace HMS.ContractsMicroService.Core.Interfaces.Repository
{
    public interface IWorkHoursRepository : IBaseRepository<WorkHours>
    {
        Task<WorkHours> FindWorkHours(WorkHours workHours);
    }
}
