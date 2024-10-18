using HMS.ContractsMicroService.Core.Interfaces.Settings;
using HMS.ContractsMicroService.Core.Json;
using HMS.ContractsMicroService.Infrastructure.Context;
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
        public async Task Delete()
        {
            await _context.DeleteAsync();
        }

        public async Task<T> Get<T>()
        {
            try
            {
                return await _context.GetSettings<T>();
            }
            catch
            {
                return default;
            }
        }

        public async Task Put(object settings)
        {
            // Devido ao metodo inserir e atualizar
            await Register(settings);
        }

        public async Task Register(object settings)
        {
            var dataJson = await JsonManipulation.Serialize(settings);
            var dataBytes = Encoding.UTF8.GetBytes(dataJson);
            await _context.InsertOrUpdate(dataBytes);
        }
    }
}
