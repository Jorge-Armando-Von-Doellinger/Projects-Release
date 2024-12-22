using System.ComponentModel.DataAnnotations;
using OmniSphere.Payments.Core.Enums;

namespace OmniSphere.Payments.Core.Entity;

public class Payment : BaseEntity
{
    private string _accountId;

    public required string AccountId
    {
        get => _accountId;
        set
        {
            if(value?.Length <= 1) throw new ArgumentException("Account Id is too short");
            _accountId = value;
        }
    }
    public double Amount { get; set; }
    public PaymentStatusEnum Status { get; set; }
    public string? Message { get; set; }
}