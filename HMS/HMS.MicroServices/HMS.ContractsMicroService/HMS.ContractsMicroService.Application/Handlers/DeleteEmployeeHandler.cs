using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Application.Interfaces.Messaging;

namespace HMS.ContractsMicroService.Application.Handlers
{
    internal sealed class DeleteEmployeeHandler : IMessageHandler
    {
        private readonly IEmployeeContractManager _manager;

        public DeleteEmployeeHandler(IEmployeeContractManager manager)
        {
            _manager = manager;
        }

        public Type DtoType => typeof(string);

        public async Task HandleAsync(object input)
        {
            if(input is string value)
                await _manager.Delete(value);
            else
                throw new InvalidDataException("Input for delete isn't compatible");
        }
    }
}
