using OmniSphere.OrderState.Application.DTOs;
using OmniSphere.OrderState.Core.Enum;

namespace OmniSphere.OrderState.Application.Mapper;

public class OrderStateMapper
{
    internal Core.Entity.OrderState Map(OrderStateDto orderState)
    {
        return new()
        {
            State = (StateEnum)Enum.ToObject(typeof(StateEnum), orderState.State),
            AccountId = orderState.AccountId,
        };
    }
    
    internal IEnumerable<OrderStateWithIdDto> Map(IEnumerable<Core.Entity.OrderState> orderStates)
        => orderStates.Select(Map);
    
    internal OrderStateWithIdDto Map(Core.Entity.OrderState orderState)
    {
        return new()
        {
            State = (int)orderState.State,
            AccountId = orderState.AccountId,
            Id = orderState.Id,
        };
    }
}