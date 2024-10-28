using System.ComponentModel.DataAnnotations;

namespace HMS.Payments.Infrastructure.Settings.Interfaces
{
    public interface IDatabaseSettings
    {
        public string ConnectionString { get; }
        [MaxLength(50)]
        public string DatabaseName { get; }
        [MaxLength(50)]
        public string PaymentEmployeeCollection { get; }
        [MaxLength(50)]
        public string PaymentCollection { get; }

    }
}
