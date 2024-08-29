using HMS.Employee.Application.Mappers;
using HMS.Employee.Application.Response;
using HMS.Employee.Core.Entity;
using HMS.Employee.Core.Interface.Manager;
using HMS.Employee.Core.Interface.Repository;
using Nuget.Employee.Inputs;

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
                await _repository.Add(await EmployeeMapper.Map(input));
                return await ResponseUseCase.GetResponseSuccess();
            }
            catch (Exception ex)
            {
                return await ResponseUseCase.GetResponseError(ex.Message);
            }
        }
        public Task<Nuget.Response.Response> Update(EmployeeInput updateInput, Guid ID)
        {
            throw new NotImplementedException();
        }

        public Task<Nuget.Response.Response> DeleteById(Guid ID)
        {
            throw new NotImplementedException();
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
                throw;
                return await ResponseUseCase.GetResponseError(ex.Message);
            }
        }

        public Task<Nuget.Response.Response> GetById(Guid ID)
        {
            throw new NotImplementedException();
        }

    }
}
