using HMS.ContractsMicroService.Application.DTOs.Input;
using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Entity.Base;
using HMS.ContractsMicroService.Core.Extensions;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Core.Json;
using HMS.ContractsMicroService.Infrastructure.Messages;

namespace HMS.ContractsMicroService.Application.Manager
{
    public sealed class EmployeeContractsManager : IEmployeeContractManager 
    {
        private readonly IWorkHoursManager _workHoursManager;
        private readonly IEmployeeContractRepository _repository;

        public EmployeeContractsManager(IEmployeeContractRepository repository, IWorkHoursManager workHoursManager)
        {
            _repository = repository;
            _workHoursManager = workHoursManager;   
        }

        public async Task Add(EmployeeContractInput input)
        {
            if (await _workHoursManager.GetById(input.WorkHoursID) == null)
                throw new Exception(MessageRecords.KeyNotFounded);

            var employeeContract = await Task.Run(() => input.FromTo<EmployeeContract>());
            await _repository.AddAsync(employeeContract);
        }

        public async Task Delete(string entityId)
        {
            await _repository.DeleteAsync(entityId);
        }

        public async Task<EmployeeContractOutput> GetById(string entityId)
        {
            var contract = await _repository.GetByIdAsync(entityId);
            return await Task.Run(() => contract.FromTo<EmployeeContractOutput>());
        }

        public async Task<List<EmployeeContractOutput>> GetAll()
        {
            var contracts = await _repository.GetAsync();
            return await Task.Run(() => contracts.FromTo<List<EmployeeContractOutput>>());
        }

        public async Task Update(EmployeeContractUpdateInput input)
        {
            if (await _workHoursManager.GetById(input.WorkHoursID) == null) // ARrumar
                throw new Exception(MessageRecords.KeyNotFounded);
            await _repository.UpdateAsync(await Task.Run(() => (EmployeeContract) input.FromTo(typeof(EmployeeContract), typeof(EntityBase))));
        }
    }
}
