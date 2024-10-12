using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Application.Interfaces.Messaging;
using HMS.ContractsMicroService.Core.Entity;

namespace HMS.ContractsMicroService.Application.Handlers
{
    internal sealed class DeleteContractHandler : IMessageHandler
    {
        private readonly IContractManager _manager;

        public DeleteContractHandler(IContractManager manager)
        {
            _manager = manager;
        }

        public Type DtoType => typeof(string);

        public async Task HandleAsync(object input)
        {
            if (input is string ID)
                await _manager.Delete(ID);
            else
                throw new InvalidDataException("Input for add isn't compatible");
        }
    }
}
