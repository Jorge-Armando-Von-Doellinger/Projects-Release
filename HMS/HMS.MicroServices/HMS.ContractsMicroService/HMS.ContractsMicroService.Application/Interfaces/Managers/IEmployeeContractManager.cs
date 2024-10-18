using HMS.ContractsMicroService.Application.DTOs.Input;
using HMS.ContractsMicroService.Application.DTOs.Output;
using HMS.ContractsMicroService.Application.DTOs.UpdateInput;
using HMS.ContractsMicroService.Core.Entity;

namespace HMS.ContractsMicroService.Application.Interfaces.Managers
{
    public interface IEmployeeContractManager : IManager<EmployeeContractInput, EmployeeContractUpdateInput, EmployeeContractOutput>
    {

    }
}
