using HMS.Payments.Core.Data;

namespace HMS.Payments.Core.Interfaces.Services
{
    public interface IServiceDiscovery
    {
        Task RegisterService(ServiceData data);
        Task DeRegisterService(string serviceId);
        Task<ServiceData> GetServiceDataById(string serviceId);
    }
}
