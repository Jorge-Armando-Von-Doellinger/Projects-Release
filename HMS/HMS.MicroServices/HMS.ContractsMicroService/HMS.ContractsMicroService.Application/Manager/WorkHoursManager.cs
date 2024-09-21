using HMS.ContractsMicroService.Application.Interfaces;
using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Extensions;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using Nuget.Contracts.Inputs;

namespace HMS.ContractsMicroService.Application.Manager
{
    public sealed class WorkHoursManager : IWorkHoursManager
    {
        private readonly IWorkHoursRepository _repository;

        public WorkHoursManager(IWorkHoursRepository repository)
        {
            _repository = repository;
        }
        public Task Add(WorkHoursInput entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task<WorkHours> FindByWorkHours(WorkHoursInput input)
        {
            return _repository.FindWorkHours(input.FromTo<WorkHours>());
        }

        public Task<List<WorkHours>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<WorkHours> GetById(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public Task Update(WorkHoursInput entity)
        {
            throw new NotImplementedException();
        }
    }
}
