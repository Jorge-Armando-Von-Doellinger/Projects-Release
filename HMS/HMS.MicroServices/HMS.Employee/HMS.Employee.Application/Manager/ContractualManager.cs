
using HMS.Employee.Application.Response;
using HMS.Employee.Core.Entity;
using HMS.Employee.Core.Enum;
using HMS.Employee.Core.Interface.Manager;
using HMS.Employee.Core.Interface.Repository;
using HMS.Employee.Core.Json;
using HMS.Employee.Core.Mapper;
using Nuget.Employee.Inputs;

namespace HMS.Employee.Application.Manager
{
    public sealed class ContractualManager : IManagerWithEmployeeId<Nuget.Response.Response, ContractualInformationInput<BenefitsEnum>>
    {
        private readonly IRepository<ContractualInformation> _repository;
        public ContractualManager(IRepositoryWithEmployeeId<ContractualInformation> repository)
        {
            _repository = repository;
        }
        public async Task<Nuget.Response.Response> Add(ContractualInformationInput<BenefitsEnum> input)
        {
            try
            {/*
                var contract = input.Map<ContractualInformation>();
                if (await _repository.Add(contract))
                    return await ResponseUseCase.GetResponseSuccess();*/
                return await ResponseUseCase.GetResponseError("Houve um erro durante a esta operação.");
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
                var contracts = await _repository.Get();
                var teste = contracts.FirstOrDefault();
                
                Console.WriteLine(await JsonConvert.Serialize(teste));
                await teste.UpdateAsync(new() { HourlySalaryInDollar = 1000});
                Console.WriteLine(await JsonConvert.Serialize(teste));

                return await ResponseUseCase.GetResponseSuccess(string.Empty, contracts);
            }
            catch (Exception ex)
            {
                throw;
                return await ResponseUseCase.GetResponseError(ex.Message);
            }
        }

        public Task<Nuget.Response.Response> GetByEmployeeId(Guid ID)
        {
            throw new NotImplementedException();
        }

        public Task<Nuget.Response.Response> GetById(Guid ID)
        {
            throw new NotImplementedException();
        }

        public Task<Nuget.Response.Response> Update(ContractualInformationInput<BenefitsEnum> updateInput)
        {
            throw new NotImplementedException();
        }
    }
}
