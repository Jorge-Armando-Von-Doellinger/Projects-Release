using HMS.ContractsMicroService.Application.DTOs.Input;
using HMS.ContractsMicroService.Application.DTOs.Output;
using HMS.ContractsMicroService.Application.DTOs.UpdateInput;
using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Entity.Base;
using HMS.ContractsMicroService.Core.Extensions;
using HMS.ContractsMicroService.Core.Interfaces.Repository;

namespace HMS.ContractsMicroService.Application.Manager
{
    public class ContractManager : IContractManager
    {
        private readonly IContractRepository _repository;

        public ContractManager(IContractRepository repository)
        {
            _repository = repository;
        }
        public async Task Add(ContractInput input)
        {
            var entity = input.FromTo<Contract>();
            await _repository.AddAsync(entity);
        }

        public async Task Delete(string entityId)
        {
            await _repository.DeleteAsync(entityId);
        }

        public async Task<List<ContractOutput>> GetAll()
        {
            var contracts = await _repository.GetAsync();
            return contracts.FromTo<List<ContractOutput>>();
        }

        public async Task<ContractOutput> GetById(string entityId)
        {
            var contract = await _repository.GetByIdAsync(entityId);
            return contract.FromTo<ContractOutput>();
        }

        public async Task Update(ContractUpdateInput input)
        {
            Console.WriteLine(input.ID);
            var entity = input.FromTo<Contract>(typeof(EntityBase));
            Console.WriteLine(entity.ID);
            var currentContract = await _repository.GetByIdAsync(entity.ID);
            Console.WriteLine((currentContract == null) + " Impossivel");
            currentContract.Update(entity);
            await _repository.UpdateAsync(currentContract);
        }
    }
}
