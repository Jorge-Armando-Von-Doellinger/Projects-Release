namespace OmniSphere.OrderState.Core.Interfaces.Repository;

public interface IOrderStateRepository
{
    Task CreateAsync(Entity.OrderState orderState);
    Task<Entity.OrderState> GetByIdAsync(int id);
    Task UpdateAsync(Entity.OrderState orderState);
    Task DeleteAsync(int orderStateId);
    Task<IEnumerable<Entity.OrderState>> GetAllByAccountAsync(string accountId);
}