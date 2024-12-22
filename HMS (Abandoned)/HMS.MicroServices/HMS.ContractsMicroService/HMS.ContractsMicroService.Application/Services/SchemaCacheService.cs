using HMS.ContractsMicroService.Application.DTOs.Input;
using HMS.ContractsMicroService.Application.DTOs.UpdateInput;
using HMS.ContractsMicroService.Core.Interfaces.Services;
using NJsonSchema;

namespace HMS.ContractsMicroService.Application.Services
{
    public sealed class SchemaCacheService
    {
        private readonly ICacheService _cache;

        public SchemaCacheService(ICacheService cache)
        {
            _cache = cache;
            SetSchemas();
        }

        internal JsonSchema GetCachedSchema(string nameof)
        {
            return _cache.Get<JsonSchema>(nameof) ?? throw new KeyNotFoundException("nameof not founded");
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

        private void SetContractSchema()
        {
            SetSchema<ContractInput>();
            SetSchema<ContractUpdateInput>();
            SetSchema<ContractDeleteInput>();
        }

        private void SetEmployeeContractSchema()
        {
            SetSchema<EmployeeContractInput>();
            SetSchema<EmployeeContractUpdateInput>();
            SetSchema<EmployeeContractDeleteInput>();
        }
    }
}
