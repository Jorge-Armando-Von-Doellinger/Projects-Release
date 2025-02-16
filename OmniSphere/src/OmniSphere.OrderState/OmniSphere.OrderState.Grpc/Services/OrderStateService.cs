using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OmniSphere.OrderState.Application.Interfaces.UseCases;
using OmniSphere.OrderState.Grpc.ProtoMapper;

namespace OmniSphere.OrderState.Grpc.Services;

public class OrderStateService : OrderState.OrderStateBase
{
    private readonly IOrderStateUseCase _useCase;
    private readonly OrderStateProtoDtoMapper _mapper;

    public OrderStateService(IOrderStateUseCase useCase,
        OrderStateProtoDtoMapper mapper)
    {
        _useCase = useCase;
        _mapper = mapper;
    }
    public override async Task<Empty> AddOrderState(OrderStateMessage request, ServerCallContext context)
    {
        var dto = _mapper.ToOrderStateDto(request);
        await _useCase.CreateAsync(dto);
        return new Empty();
    }

    public override async Task<Empty> DeleteOrderState(OrderId request, ServerCallContext context)
    {
        await _useCase.DeleteAsync(request.Id);
        return new Empty();
    }

    public override async Task<Empty> UpdateOrderState(OrderStateWithIdMessage request, ServerCallContext context)
    {
        var dto = _mapper.ToOrderStateWithIdDto(request);
        await _useCase.UpdateAsync(dto);
        return new Empty();
    }

    public override async Task<OrderStateWithIdMessage> GetOrderById(OrderId request, ServerCallContext context)
    {
        var output = await _useCase.GetByIdAsync(request.Id);
        return new()
        {
            Id = output.Id,
            AccountId = output.AccountId,
            State = output.State,
        };
    }

    public override async Task<ListOrderStateMessages> GetOrdersByAccountId(AccountId request, ServerCallContext context)
    {
        var orderStates = await _useCase.GetAllByAccountIdAsync(request.Id);
        var output = orderStates.Select(x => new OrderStateWithIdMessage()
        {
            AccountId = x.AccountId,
            State = x.State,
            Id = x.Id,
        });
        var list = new ListOrderStateMessages();
        list.OrderStateMessages.AddRange(output);
        return list;
    }
    
}