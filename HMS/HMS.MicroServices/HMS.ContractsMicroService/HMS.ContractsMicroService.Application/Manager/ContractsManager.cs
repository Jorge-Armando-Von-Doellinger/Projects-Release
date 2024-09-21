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
                //var hoursExistent = await _workHoursManager.FindByWorkHours(input.WorkHours);
                /*if(hoursExistent != null)
                {
                    //input.WorkHoursID = hoursExistent.ID;
                    //input.WorkHours = null;
                    Console.WriteLine(hoursExistent.ID);
                    Console.WriteLine("     NAO NULO     ");
                }*/

                var w = new WorkHours() { EntryTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)) };
                input.WorkHours = w.FromTo<WorkHoursInput>();
                var e = await Task.Run(() => input.FromTo<EmployeeContract>());
                e.Update(new EmployeeContract { WorkHours = w });
                await Task.Delay(5000);
                Console.WriteLine(await JsonManipulation.Serialize(e) + "Batata");
                await _repository.AddAsync(e);
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
