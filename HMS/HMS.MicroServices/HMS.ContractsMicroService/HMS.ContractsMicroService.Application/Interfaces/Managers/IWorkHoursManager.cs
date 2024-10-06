using HMS.ContractsMicroService.Core.Entity;
using Nuget.Contracts.Inputs;
using Nuget.Contracts.Outputs;

namespace HMS.ContractsMicroService.Application.Interfaces.Managers
{
    public interface IWorkHoursManager : IManager<WorkHoursInput, WorkHoursUpdateInput, WorkHoursOutput>
    {
        public Task<WorkHoursOutput> FindByWorkHours(WorkHoursInput input);
    }
}
