using HMS.Payments.Infrastructure.Settings.Interfaces;

namespace HMS.Payments.Infrastructure.Settings.Implementations
{
    public sealed class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string PaymentEmployeeCollection { get; set; }

        public string PaymentCollection { get; set; }

        public string Name { get; set; }
    }
}
