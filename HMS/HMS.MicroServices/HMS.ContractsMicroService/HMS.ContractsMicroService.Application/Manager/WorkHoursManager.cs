using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Extensions;
using HMS.ContractsMicroService.Core.Interfaces.Repository;

namespace HMS.ContractsMicroService.Application.Manager
{
    public sealed class WorkHoursManager : IWorkHoursManager
    {
        private readonly IWorkHoursRepository _repository;


        public WorkHoursManager(IWorkHoursRepository repository)
        {
            _repository = repository;
        }
        public async Task Add(WorkHours entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task Delete(string entityId)
        {
            await _repository.DeleteAsync(entityId);
        } 

        public async Task<WorkHours> FindByWorkHours(WorkHours input)
        {
            var workHours =  await _repository.FindWorkHours(input);
            return workHours;
        }

        public async Task<List<WorkHours>> GetAll()
        {
            var workhours = await _repository.GetAsync();
            return workhours;
        }

        public async Task<WorkHours> GetById(string entityId)
        {
            var workhours =  await _repository.GetByIdAsync(entityId);
            return workhours;
        }

        public async Task Update(WorkHours entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
