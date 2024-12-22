using System.ComponentModel.DataAnnotations;
using HMS.Payments.Core.Entity.Base;

namespace HMS.Payments.Core.Entity;

public class Refund : EntityBase
{
    public double Amount { get; set; }
    public string? Reason { get; set; }

    [StringLength(36, MinimumLength = 32)] public string PaymentId { get; set; }
}