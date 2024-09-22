using HMS.ContractsMicroService.Core.Entity;
using Nuget.Contracts.Inputs;
using Nuget.Response;

namespace HMS.ContractsMicroService.Application.Interfaces
{
    public interface IEmployeeContractManager : IManager<EmployeeContractInput ,Response>
    {

    }
}
