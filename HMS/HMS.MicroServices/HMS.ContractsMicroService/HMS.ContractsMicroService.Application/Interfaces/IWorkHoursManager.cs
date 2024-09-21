using HMS.ContractsMicroService.Core.Entity;
using Nuget.Contracts.Inputs;

namespace HMS.ContractsMicroService.Application.Interfaces
{
    public interface IWorkHoursManager : IManager<WorkHoursInput, WorkHours>
    {
        public Task<WorkHours> FindByWorkHours(WorkHoursInput input);
    }
}
