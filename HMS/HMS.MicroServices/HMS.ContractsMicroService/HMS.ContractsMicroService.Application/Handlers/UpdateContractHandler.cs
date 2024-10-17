using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Application.Interfaces.Messaging;

namespace HMS.ContractsMicroService.Application.Handlers
{
    internal sealed class UpdateContractHandler : IMessageHandler
    {
        private readonly IContractManager _manager;

        public UpdateContractHandler(IContractManager manager)
        {
            this._manager = manager;
        }

        public Type DtoType => typeof(ContractUpdateInput);

        public async Task HandleAsync(object input)
        {
            if(input is ContractUpdateInput contract)
                await _manager.Update(contract);
            else
                throw new InvalidDataException("Input for add isn't compatible");
        }
    }
}
