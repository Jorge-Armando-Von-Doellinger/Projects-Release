using HMS.Payments.Infrastructure.Settings.Interfaces;

namespace HMS.Payments.Infrastructure.Settings.Implementations;

public class ServiceDiscoverySettigs : IServiceDiscoverySettings
{
    public string Address { get; set; }
}