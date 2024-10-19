using HMS.ContractsMicroService.Application.DTOs.Input;
using HMS.ContractsMicroService.Application.DTOs.UpdateInput;
using HMS.ContractsMicroService.Core.Interfaces.Services;
using NJsonSchema;

namespace HMS.ContractsMicroService.API.Services.Hosted
{
    public class SetDtosSchemas : IHostedService
    {
        private readonly ICacheService _cache;

        public SetDtosSchemas(ICacheService cache)
        {
            _cache = cache;
        }

        private void SetSchemas()
        {
            SetContractSchema();
            SetEmployeeContractSchema();
        }

        private void SetSchema<T>() where T : class
        {
            var schema = JsonSchema.FromType<T>();

            _cache.Set(nameof(T), schema);

        }

        internal void SetContractSchema()
        {
            SetSchema<ContractInput>();
            SetSchema<ContractUpdateInput>();
            SetSchema<ContractDeleteInput>();
        }

        internal void SetEmployeeContractSchema()
        {
            SetSchema<EmployeeContractInput>();
            SetSchema<EmployeeContractUpdateInput>();
            SetSchema<EmployeeContractDeleteInput>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            SetSchemas();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cache.RemoveAll();
            return Task.CompletedTask;
        }
    }
}
