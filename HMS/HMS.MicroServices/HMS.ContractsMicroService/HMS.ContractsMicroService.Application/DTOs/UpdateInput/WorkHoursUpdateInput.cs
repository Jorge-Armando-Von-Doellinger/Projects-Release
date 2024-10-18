using HMS.ContractsMicroService.Application.DTOs.Input;

namespace HMS.ContractsMicroService.Application.DTOs.UpdateInput
{
    public class WorkHoursUpdateInput : WorkHoursInput
    {
        public string ID { get; set; } = string.Empty;
    }
}
