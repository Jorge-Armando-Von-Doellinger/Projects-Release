using HMS.ContractsMicroService.Application.DTOs.Input;
using HMS.ContractsMicroService.Application.DTOs.Output;
using HMS.ContractsMicroService.Application.DTOs.UpdateInput;

namespace HMS.ContractsMicroService.Application.Interfaces.Managers
{
    public interface IContractManager : IManager<ContractInput, ContractUpdateInput, ContractOutput>
    {

    }
}
