using HMS.ContractsMicroService.Core.Entity;
using Nuget.Contracts.Inputs;

namespace HMS.ContractsMicroService.Application.Interfaces.Managers
{
    public interface IContractManager : IManager<ContractInput, ContractUpdateInput, ContractUpdateInput>
    {

    }
}
