using Consul;
using HMS.Payments.Core.Data;
using HMS.Payments.Core.Interfaces.Services;
using HMS.Payments.Infrastructure.Context;

namespace HMS.Payments.Infrastructure.Services
{
    public sealed class ConsulServiceDiscovery : IServiceDiscovery
    {
        private readonly ConsulContext _context;

        public ConsulServiceDiscovery(ConsulContext context)
        {
            _context = context; 
        }
        public async Task DeRegisterService(string serviceId)
        {
            await _context.Agent.ServiceDeregister(serviceId);
        }

        public async Task<ServiceData> GetServiceDataById(string serviceId)
        {
            var services = await _context.Agent.Services();
            var data = services.Response
                .Where(x => x.Key == serviceId)
                .Select(x => x.Value)
                .FirstOrDefault() ?? throw new KeyNotFoundException("Service not founded!");
            return new()
            {
                Address = data?.Address,
                ID = data?.ID,
                Port = data.Port,
                Name = data?.Service,
            };
        }

        public async Task RegisterService(ServiceData data)
        {
            var registration = new AgentServiceRegistration()
            {
                Address = data?.Address,
                ID = data?.ID,
                Name = data.Name,
                Port = data.Port,
                Check = new()
                {
                    HTTP = data?.Check.HTTP,
                    Interval = data?.Check.Interval,
                    Timeout = data?.Check.Timeout,
                }
            };
            await _context.Agent.ServiceRegister(registration);
        }
    }
}
