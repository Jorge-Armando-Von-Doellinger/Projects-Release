using HMS.ContractsMicroService.Application.DTOs.Input;
using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Application.Interfaces.Messaging;

namespace HMS.ContractsMicroService.Application.Handlers
{
    internal sealed class AddContractHandler : IMessageHandler
    {
        private readonly IContractManager _manager;
        public AddContractHandler(IContractManager manager)
        {
            _manager = manager;
        }

        public Type DtoType => typeof(ContractInput);

        public async Task HandleAsync(object input)
        {
            if (input is ContractInput contract) await _manager.Add(contract);
            else throw new InvalidDataException("Input for add isn't compatible");
        }
    }
}
