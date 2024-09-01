using HMS.Employee.Application.Mappers;
using HMS.Employee.Application.Response;
using HMS.Employee.Core.Entity;
using HMS.Employee.Core.Interface.Manager;
using HMS.Employee.Core.Interface.Repository;
using Nuget.Employee.Inputs;
using System.Text.Json;

namespace HMS.Employee.Application.Manager
{
    public sealed class EmployeeManager : IManager<Nuget.Response.Response, EmployeeInput> // DTO
    {
        private readonly IRepository<Core.Entity.Employee> _repository;
        public EmployeeManager(IRepository<Core.Entity.Employee> repository)
        {
            _repository = repository;
        }
        public async Task<Nuget.Response.Response> Add(EmployeeInput input)
        {
            try
            {
                var employee = await ObjectMapper.Mapper<Core.Entity.Employee>(input);
                await _repository.Add(employee);
                return await ResponseUseCase.GetResponseSuccess();
            }
            catch (Exception ex)
            {
                throw; // §
                return await ResponseUseCase.GetResponseError(ex.Message);
            }
        }
        public async Task<Nuget.Response.Response> Update(EmployeeInput updateInput)
        {
            try
            {
                var employee = await ObjectMapper.Mapper<Core.Entity.Employee>(updateInput);
                await _repository.Update(employee);
                return await ResponseUseCase.GetResponseSuccess();
            }
            catch (Exception ex)
            {
                throw; // §
                return await ResponseUseCase.GetResponseError(ex.Message);
            }
        }

        public async Task<Nuget.Response.Response> DeleteById(Guid ID)
        {
            try
            {
                await _repository.Update(new () { Id = ID });
                return await ResponseUseCase.GetResponseSuccess();
            }
            catch (Exception ex)
            {
                throw; // §
                return await ResponseUseCase.GetResponseError(ex.Message);
            }
        }

        public async Task<Nuget.Response.Response> Get()
        {
            try
            {
                var data = await _repository.Get();
                return await ResponseUseCase.GetResponseSuccess("", data);
            }
            catch (Exception ex)
            {
                throw; // §
                return await ResponseUseCase.GetResponseError(ex.Message);
            }
        }

        public async Task<Nuget.Response.Response> GetById(Guid ID)
        {
            try
            {
                var employee = await _repository.GetById(ID)
                    ?? throw new ArgumentNullException(nameof(Core.Entity.Employee), "Funcionario não encontrado!");
                return await ResponseUseCase.GetResponseSuccess(null, employee);
            }
            catch (Exception ex1)
            {
                throw; // §
                return await ResponseUseCase.GetResponseError(ex1.Message);
            }
        }

    }
}
