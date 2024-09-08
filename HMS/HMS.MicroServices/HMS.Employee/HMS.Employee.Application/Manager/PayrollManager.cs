using HMS.Employee.Application.Mappers;
using HMS.Employee.Application.Response;
using HMS.Employee.Core.Data.Discounts;
using HMS.Employee.Core.Entity;
using HMS.Employee.Core.Enum;
using HMS.Employee.Core.Extensions;
using HMS.Employee.Core.Interface.Manager;
using HMS.Employee.Core.Interface.Repository;
using HMS.Employee.Core.Json;
using HMS.Employee.Core.Mapper;
using Nuget.Employee.Inputs;
using Nuget.Response;

namespace HMS.Employee.Application.Manager
{
    public sealed class PayrollManager : IManagerWithEmployeeId<Nuget.Response.Response, PayrollInput<BenefitsEnum, Discount>>
    {
        private readonly IRepositoryWithEmployeeId<Payroll> _repository;
        public PayrollManager(IRepositoryWithEmployeeId<Payroll> repository)
        {
            _repository = repository;
        }

        public async Task<Nuget.Response.Response> Add(PayrollInput<BenefitsEnum, Discount> input)
        {
            try
            {
                var payroll = await ObjectMapper.Mapper<Payroll>((object)input);
                if (await _repository.Add(payroll))
                    return await ResponseUseCase.GetResponseSuccess();
                throw new Exception("Houve um erro ao adicionar esta folha de pagamento. Por favor, tente mais tarde!");
            }
            catch (Exception ex)
            {
                throw;
                return await ResponseUseCase.GetResponseError(ex.Message);
            }
        }

        public Task<Nuget.Response.Response> DeleteById(Guid ID)
        {
            throw new NotImplementedException();
        }

        public async Task<Nuget.Response.Response> Get()
        {
            try
            {
                var payroll = await _repository.Get();
                var dataToResponse = payroll.Map<PayrollInput<BenefitsEnum, Discount>>();
                await Task.Delay(1000);
                Console.WriteLine(await JsonConvert.Serialize(payroll));
                Console.WriteLine();
                Console.WriteLine(await JsonConvert.Serialize(dataToResponse));
                return await ResponseUseCase.GetResponseSuccess(string.Empty, dataToResponse);
            }
            catch (Exception ex)
            {
                throw;
                return await ResponseUseCase.GetResponseError(ex.Message);
            }
        }

        public async Task<Nuget.Response.Response> GetByEmployeeId(Guid ID)
        {
            try
            {
                var employee = await _repository.GetByEmployeeId(ID);
                if (employee.Count == 0)
                    throw new Exception("Nenhuma folha de pagamento encontrada");
                return await ResponseUseCase.GetResponseSuccess(null, employee);
            }
            catch (Exception ex)
            {
                return await ResponseUseCase.GetResponseError(ex.Message);
            }
        }

        public Task<Nuget.Response.Response> GetById(Guid ID)
        {
            throw new NotImplementedException();
        }

        public Task<Nuget.Response.Response> Update(PayrollInput<BenefitsEnum, Discount> updateInput)
        {
            throw new NotImplementedException();
        }
    }
}
