using HMS.ContractsMicroService.Application.Interfaces;
using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Extensions;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using Nuget.Contracts.Inputs;

namespace HMS.ContractsMicroService.Application.Manager
{
    public sealed class ContractsManager : IEmployeeContractManager
    {
        private readonly IEmployeeContractRepository _repository;

        public ContractsManager(IEmployeeContractRepository repository)
        {
            _repository = repository;
        }

        public async Task Add(EmployeeContractInput entity)
        {
            try
            {
                await _repository.AddAsync(entity.FromTo<EmployeeContract>());
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public async Task Delete(Guid entityId)
        {
            try
            {
                await _repository.DeleteAsync(entityId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<EmployeeContract> GetById(Guid entityId)
        {
            try
            {
                return await _repository.GetByIdAsync(entityId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<EmployeeContract>> GetAll()
        {
            return await _repository.GetAsync();
        }

        public async Task Update(EmployeeContractInput entity)
        {
            try
            {
                await _repository.UpdateAsync(entity.FromTo<EmployeeContract>());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
