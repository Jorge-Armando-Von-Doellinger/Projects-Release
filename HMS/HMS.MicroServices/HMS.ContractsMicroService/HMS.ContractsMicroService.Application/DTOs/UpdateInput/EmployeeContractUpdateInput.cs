using HMS.ContractsMicroService.Application.DTOs.Input;

namespace HMS.ContractsMicroService.Application.DTOs.UpdateInput
{
    public class EmployeeContractUpdateInput : EmployeeContractInput
    {
        public string ID { get; set; } = string.Empty;
    }
}
