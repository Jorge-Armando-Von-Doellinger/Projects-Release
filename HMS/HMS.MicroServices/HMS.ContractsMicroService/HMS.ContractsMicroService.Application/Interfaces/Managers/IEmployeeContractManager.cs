using HMS.ContractsMicroService.Core.Entity;
using Nuget.Contracts.Inputs;
using Nuget.Contracts.Outputs;

namespace HMS.ContractsMicroService.Application.Interfaces.Managers
{
    public interface IEmployeeContractManager : IManager<EmployeeContractInput, EmployeeContractUpdateInput, EmployeeContractOutput>
    {

    }
}
