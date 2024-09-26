﻿using HMS.ContractsMicroService.Application.Interfaces;
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
        public async Task Add(WorkHoursInput entity)
        {
            var existingHourst = await FindByWorkHours(entity);
            if (existingHourst == null)
                await _repository.AddAsync(await Task.Run(() => entity.FromTo<WorkHours>()));
            
        }

        public async Task Delete(Guid entityId)
        {
            await _repository.DeleteAsync(entityId);
        } 

        public async Task<WorkHours> FindByWorkHours(WorkHoursInput input)
        {
            return await _repository.FindWorkHours(await Task.Run(() => input.FromTo<WorkHours>()));
        }

        public async Task<List<WorkHours>> GetAll()
        {
            return await _repository.GetAsync();
        }

        public Task<WorkHours> GetById(Guid entityId)
        {
            return _repository.GetByIdAsync(entityId);
        }

        public async Task Update(WorkHoursUpdateInput entity)
        {
            await _repository.UpdateAsync(await Task.Run(() => entity.FromTo<WorkHours>()));
        }
    }
}
