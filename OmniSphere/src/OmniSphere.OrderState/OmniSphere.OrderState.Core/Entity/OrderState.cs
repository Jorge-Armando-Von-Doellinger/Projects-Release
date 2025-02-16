using OmniSphere.OrderState.Core.Enum;

namespace OmniSphere.OrderState.Core.Entity;

public class OrderState : BaseEntity
{
    public string AccountId { get; set; } 
    public StateEnum State { get; set; }       
    public DateTime CompletedAt { get; set; }
}