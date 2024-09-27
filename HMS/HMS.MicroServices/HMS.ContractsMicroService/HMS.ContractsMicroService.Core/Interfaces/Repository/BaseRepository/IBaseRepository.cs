using HMS.ContractsMicroService.Core.Entity;

namespace HMS.ContractsMicroService.Core.Interfaces.Repository.BaseRepository
{
    public interface IBaseRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid entityId);
        Task<TEntity> GetByIdAsync(Guid entityId);
        Task<TEntity[]> GetAsync();
    }
}
