using HMS.ContractsMicroService.Application.DTOs.Input;

namespace HMS.ContractsMicroService.Application.DTOs.UpdateInput
{
    public sealed class ContractUpdateInput : ContractInput
    {
        public string ID { get; set; } = string.Empty;
    }
}
