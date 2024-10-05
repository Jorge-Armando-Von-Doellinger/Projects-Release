using HMS.ContractsMicroService.Core.Interfaces.Settings;
using HMS.ContractsMicroService.Core.Json;
using HMS.ContractsMicroService.Infrastructure.Context;
using Nuget.Settings;
using System.Text;

namespace HMS.ContractsMicroService.Infrastructure.Services
{
    public sealed class DiscoveryServiceConsul : IDiscoveryService
    {
        private readonly ConsulContext _context;

        public DiscoveryServiceConsul(ConsulContext context)
        {
            _context = context;
        }
        public async Task Delete(string myAppName)
        {
            await _context.DeleteAsync(myAppName);
        }

        public async Task<T> Get<T>(string myAppName)
        {
            return await _context.GetSettings<T>(myAppName);
        }

        public async Task Put(object settings, string myAppName)
        {
            // Devido ao metodo inserir e atualizar
            await Register(settings, myAppName);
        }

        public async Task Register(object settings, string myAppName)
        {
            var dataJson = await JsonManipulation.Serialize(settings);
            var dataBytes = Encoding.UTF8.GetBytes(dataJson);
            await _context.InsertOrUpdate(dataBytes, myAppName);
        }
    }
}
