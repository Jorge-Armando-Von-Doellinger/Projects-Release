using HMS.ContractsMicroService.Application.Interfaces;
using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Entity.Base;
using HMS.ContractsMicroService.Core.Extensions;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Core.Json;
using HMS.ContractsMicroService.Infrastructure.Messages;
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
            if (await _workHoursManager.GetById(input.WorkHoursID.ToString()) == null)
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
            //contracts.FromTo<EmployeeContractOutput[]>();
            return await Task.Run(() => contracts.FromTo<List<EmployeeContractOutput>>());
        }

        public async Task Update(EmployeeContractUpdateInput input)
        {
            if (await _workHoursManager.GetById(input.WorkHoursID.ToString()) == null) // ARrumar
                throw new Exception(MessageRecords.KeyNotFounded);
            await _repository.UpdateAsync(await Task.Run(() => (EmployeeContract) input.FromTo(typeof(EmployeeContract), typeof(EntityBase))));
        }
    }
}
