using HMS.ContractsMicroService.Application.DTOs.Input;
using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Application.Interfaces.Messaging;

namespace HMS.ContractsMicroService.Application.Handlers
{
    internal class AddEmployeeHandler : IMessageHandler
    {
        private readonly IEmployeeContractManager _manager;
        public AddEmployeeHandler(IEmployeeContractManager manager)
        {
            _manager = manager;
        }

        public Type DtoType => typeof(EmployeeContractInput);

        public async Task HandleAsync(object input)
        {
            if(input is EmployeeContractInput value)
                await _manager.Add(value);
            else
                throw new InvalidDataException("Input for add isn't compatible");
        }
    }
}
