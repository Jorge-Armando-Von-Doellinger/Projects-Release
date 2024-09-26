namespace HMS.ContractsMicroService.Application.Interfaces
{
    public interface IManager<TEntity>
    {
        Task Add(TEntity entity);
        Task Delete(Guid entityId);
        Task Update(TEntity entity);
        Task<TEntity> GetById(Guid entityId);
        Task<List<TEntity>> GetAll();
    }
    public interface IManager<TInput, TOutput>
    {
        Task Add(TInput input);
        Task Delete(Guid entityId);
        Task Update(TInput input);
        Task<TOutput> GetById(Guid entityId);
        Task<List<TOutput>> GetAll();
    }
    public interface IManager<TInput, TUpdateInput,TOutput>
    {
        Task Add(TInput input);
        Task Delete(Guid entityId);
        Task Update(TUpdateInput input);
        Task<TOutput> GetById(Guid entityId);
        Task<List<TOutput>> GetAll();
    }
}
