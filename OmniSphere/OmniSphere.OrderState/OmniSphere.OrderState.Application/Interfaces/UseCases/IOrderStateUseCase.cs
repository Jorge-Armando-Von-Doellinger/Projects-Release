using OmniSphere.OrderState.Application.DTOs;

namespace OmniSphere.OrderState.Application.Interfaces.UseCases;

public interface IOrderStateUseCase
{
    Task CreateAsync(OrderStateDto orderState);
    Task UpdateAsync(OrderStateDto orderState);
    Task DeleteAsync(int id);
    Task<OrderStateWithIdDto> GetByIdAsync(int id);
    Task<IEnumerable<OrderStateWithIdDto>> GetAllByAccountIdAsync(string accountId);
}