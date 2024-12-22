using OmniSphere.OrderState.Application.DTOs;
using OmniSphere.OrderState.Application.Interfaces.UseCases;
using OmniSphere.OrderState.Application.Mapper;
using OmniSphere.OrderState.Core.Interfaces.Repository;

namespace OmniSphere.OrderState.Application.UseCases;

public class OrderStateUseCase : IOrderStateUseCase
{
    private readonly IOrderStateRepository _repository;
    private readonly OrderStateMapper _mapper;

    public OrderStateUseCase(IOrderStateRepository repository,
        OrderStateMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task CreateAsync(OrderStateDto orderState)
    {
        var entity = _mapper.Map(orderState);
        await _repository.CreateAsync(entity);
    }

    public async Task UpdateAsync(OrderStateDto orderState)
    {
        var entity = _mapper.Map(orderState);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<OrderStateWithIdDto> GetByIdAsync(int id)
    {
        var orderStates = await _repository.GetByIdAsync(id);
        var output = _mapper.Map(orderStates);
        return output;
    }

    public async Task<IEnumerable<OrderStateWithIdDto>> GetAllByAccountIdAsync(string accountId)
    {
        var orderStates = await _repository.GetAllByAccountAsync(accountId);
        var output = _mapper.Map(orderStates);
        return output;
    }
}