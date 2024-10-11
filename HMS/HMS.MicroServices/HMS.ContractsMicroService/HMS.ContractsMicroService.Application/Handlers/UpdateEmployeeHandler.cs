using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Application.Interfaces.Messaging;
using Nuget.Contracts.Inputs;

namespace HMS.ContractsMicroService.Application.Handlers
{
    internal sealed class UpdateEmployeeHandler : IMessageHandler
    {
        private readonly IEmployeeContractManager _manager;

        public UpdateEmployeeHandler(IEmployeeContractManager manager)
        {
            _manager = manager;
        }

        public Type DtoType => typeof(EmployeeContractUpdateInput);

        public async Task HandleAsync(object input)
        {
            if(input is EmployeeContractUpdateInput value)
                await _manager.Update((EmployeeContractUpdateInput)input);
            else
                throw new InvalidDataException("Input for update isn't compatible");
        }
    }
}
