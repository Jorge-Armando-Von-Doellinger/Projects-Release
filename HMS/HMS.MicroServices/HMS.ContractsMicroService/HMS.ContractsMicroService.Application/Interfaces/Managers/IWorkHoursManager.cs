using HMS.ContractsMicroService.Core.Entity;

namespace HMS.ContractsMicroService.Application.Interfaces.Managers
{
    public interface IWorkHoursManager : IManager<WorkHours>
    {
        public Task<WorkHours> FindByWorkHours(WorkHours input);
    }
}
