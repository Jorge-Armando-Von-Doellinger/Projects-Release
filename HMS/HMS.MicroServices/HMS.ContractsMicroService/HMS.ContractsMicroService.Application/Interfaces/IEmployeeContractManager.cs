using HMS.ContractsMicroService.Core.Entity;
using Nuget.Contracts.Inputs;
using Nuget.Contracts.Outputs;

namespace HMS.ContractsMicroService.Application.Interfaces
{
    public interface IEmployeeContractManager : IManager<EmployeeContractInput, EmployeeContractUpdateInput ,EmployeeContractOutput>
    {

    }
}
