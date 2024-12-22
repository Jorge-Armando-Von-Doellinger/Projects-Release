namespace HMS.ContractsMicroService.Application.Interfaces.Managers
{
    public interface IManager<TEntity>
    {
        Task Add(TEntity entity);
        Task Delete(string entityId);
        Task Update(TEntity entity);
        Task<TEntity> GetById(string entityId);
        Task<List<TEntity>> GetAll();
    }
    public interface IManager<TInput, TOutput>
    {
        Task Add(TInput input);
        Task Delete(string entityId);
        Task Update(TInput input);
        Task<TOutput> GetById(string entityId);
        Task<List<TOutput>> GetAll();
    }
    public interface IManager<TInput, TUpdateInput, TOutput>
    {
        Task Add(TInput input);
        Task Delete(string entityId);
        Task Update(TUpdateInput input);
        Task<TOutput> GetById(string entityId);
        Task<List<TOutput>> GetAll();
    }
}
