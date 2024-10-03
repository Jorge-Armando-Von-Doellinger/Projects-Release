using HMS.ContractsMicroService.Core.Interfaces.Configs;

namespace HMS.ContractsMicroService.Infrastructure.Services
{
    public sealed class SettingsService : IUserDefinition
    {
        public void Override<T>(T settings)
        {
            throw new NotImplementedException();
        }

        public void Put<T>(T settings)
        {
            throw new NotImplementedException();
        }

        public void Register<T>(T settings)
        {
            throw new NotImplementedException();
        }
    }
}
