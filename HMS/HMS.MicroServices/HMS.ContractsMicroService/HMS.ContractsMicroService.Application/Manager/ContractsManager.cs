using HMS.ContractsMicroService.Application.Interfaces;
using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Extensions;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Core.Json;
using Nuget.Contracts.Inputs;

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
                //Console.WriteLine(await JsonManipulation.Serialize(e) + "Batata");
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
