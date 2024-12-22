namespace HMS.ContractsMicroService.Core.Interfaces.Repository.BaseRepository
{
    public interface IBaseRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(string entityId);
        Task<TEntity> GetByIdAsync(string entityId);
        Task<List<TEntity>> GetAsync();
    }
}
