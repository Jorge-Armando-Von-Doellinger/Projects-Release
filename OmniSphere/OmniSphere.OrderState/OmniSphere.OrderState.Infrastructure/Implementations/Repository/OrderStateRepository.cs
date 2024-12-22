using Microsoft.EntityFrameworkCore;
using OmniSphere.OrderState.Core.Interfaces.Repository;
using OmniSphere.OrderState.Infrastructure.Context;

namespace OmniSphere.OrderState.Infrastructure.Implementations.Repository;

public class OrderStateRepository : IOrderStateRepository
{
    private readonly OrderStateContext _context;

    public OrderStateRepository(OrderStateContext context)
    {
        _context = context;
    }
    public async Task CreateAsync(Core.Entity.OrderState orderState)
    {
        await _context.OrderStates.AddAsync(orderState);
        await _context.SaveChangesAsync();
    }

    public async Task<Core.Entity.OrderState> GetByIdAsync(int id)
    {
        return await _context.OrderStates
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new KeyNotFoundException("OrderState not found");
    }

    public async Task UpdateAsync(Core.Entity.OrderState orderState)
    {
        var state = await _context.OrderStates.FindAsync(orderState.Id) ?? throw new KeyNotFoundException("OrderState not found");
        state.State = orderState.State;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int orderStateId)
    {
        _context.OrderStates.Remove(new() { Id = orderStateId });
    }

    public async Task<IEnumerable<Core.Entity.OrderState>> GetAllByAccountAsync(string accountId)
    {
        return await _context
            .OrderStates
            .AsNoTracking()
            .Where(x => x.AccountId == accountId)
            .ToArrayAsync();
    }

}