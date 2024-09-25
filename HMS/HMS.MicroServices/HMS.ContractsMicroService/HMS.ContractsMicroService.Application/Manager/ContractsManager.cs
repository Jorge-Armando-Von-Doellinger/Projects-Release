using HMS.ContractsMicroService.Application.Interfaces;
using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Extensions;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Core.Json;
using Nuget.Contracts.Inputs;
using Nuget.Contracts.Outputs;

namespace HMS.ContractsMicroService.Application.Manager
{
    public sealed class ContractsManager : IEmployeeContractManager
    {
        private readonly IWorkHoursManager _workHoursManager;
        private readonly IEmployeeContractRepository _repository;

        public ContractsManager(IEmployeeContractRepository repository, IWorkHoursManager workHoursManager)
        {
            _repository = repository;
            _workHoursManager = workHoursManager;   
        }

        public async Task Add(EmployeeContractInput input)
        {
            try
            {
                var employeeContract = await Task.Run(() => input.FromTo<EmployeeContract>());
                await _repository.AddAsync(employeeContract);
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

        public async Task<EmployeeContractOutput> GetById(Guid entityId)
        {
            try
            {
                var contract = await _repository.GetByIdAsync(entityId);
                return contract.FromTo<EmployeeContractOutput>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<EmployeeContractOutput>> GetAll()
        {
            var contracts = await _repository.GetAsync();
            return contracts.FromTo<List<EmployeeContractOutput>>();
        }

        public async Task Update(EmployeeContractInput input)
        {
            try
            {
                await _repository.UpdateAsync(input.FromTo<EmployeeContract>());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
