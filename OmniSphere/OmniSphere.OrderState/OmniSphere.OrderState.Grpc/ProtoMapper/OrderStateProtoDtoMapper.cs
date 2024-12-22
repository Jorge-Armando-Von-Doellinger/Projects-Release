using OmniSphere.OrderState.Application.DTOs;

namespace OmniSphere.OrderState.Grpc.ProtoMapper;

public class OrderStateProtoDtoMapper
{
    internal OrderStateDto ToOrderStateDto(OrderStateMessage orderState)
    {
        return new()
        {
            AccountId = orderState.AccountId,
            State = orderState.State
        };
    }

    internal OrderStateWithIdDto ToOrderStateWithIdDto(OrderStateWithIdMessage orderState)
    {
        return new()
        {
            AccountId = orderState.AccountId,
            State = orderState.State,
            Id = orderState.Id
        };
    }
}