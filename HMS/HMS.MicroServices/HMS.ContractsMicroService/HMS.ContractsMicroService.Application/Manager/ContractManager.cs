using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Core.Entity;
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
        public async Task Add(Contract entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task Delete(string entityId)
        {
            await _repository.DeleteAsync(entityId);
        }

        public async Task<List<Contract>> GetAll()
        {
            return await _repository.GetAsync();
        }

        public async Task<Contract> GetById(string entityId)
        {
            return await _repository.GetByIdAsync(entityId);
        }

        public async Task Update(Contract entity)
        {
            var currentContract = await GetById(entity.ID);
            currentContract.Update(entity);
            await _repository.UpdateAsync(currentContract);
        }
    }
}
