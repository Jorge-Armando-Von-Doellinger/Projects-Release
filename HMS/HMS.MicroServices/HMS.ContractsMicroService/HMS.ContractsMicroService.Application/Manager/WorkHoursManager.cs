using HMS.ContractsMicroService.Application.Interfaces;
using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Extensions;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using Nuget.Contracts.Inputs;
using Nuget.Contracts.Outputs;

namespace HMS.ContractsMicroService.Application.Manager
{
    public sealed class WorkHoursManager : IWorkHoursManager
    {
        private readonly IWorkHoursRepository _repository;


        public WorkHoursManager(IWorkHoursRepository repository)
        {
            _repository = repository;
        }
        public async Task Add(WorkHoursInput entity)
        {
            var existingHourst = await FindByWorkHours(entity);
            if (existingHourst == null)
                await _repository.AddAsync(await Task.Run(() => entity.FromTo<WorkHours>()));
            
        }

        public async Task Delete(string entityId)
        {
            await _repository.DeleteAsync(entityId);
        } 

        public async Task<WorkHoursOutput> FindByWorkHours(WorkHoursInput input)
        {
            var workHours =  await _repository.FindWorkHours(await Task.Run(() => input.FromTo<WorkHours>()));
            return await Task.Run(() =>workHours.FromTo<WorkHoursOutput>());
        }

        public async Task<List<WorkHoursOutput>> GetAll()
        {
            var workhours = await _repository.GetAsync();
            return await Task.Run(() =>  workhours.FromTo<List<WorkHoursOutput>>());
        }

        public async Task<WorkHoursOutput> GetById(string entityId)
        {
            var workhours =  await _repository.GetByIdAsync(entityId);
            return await Task.Run(() => workhours.FromTo<WorkHoursOutput>());
        }

        public async Task Update(WorkHoursUpdateInput entity)
        {
            await _repository.UpdateAsync(await Task.Run(() => entity.FromTo<WorkHours>()));
        }
    }
}
