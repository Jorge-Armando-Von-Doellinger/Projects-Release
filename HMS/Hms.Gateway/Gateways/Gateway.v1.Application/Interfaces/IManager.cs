namespace Gateway.v1.Application.Interfaces
{
    public interface IManager
    {
        Task<bool> Add(object input);
        Task<bool> Update(object input);
        Task<bool> Delete(long ID);
        Task<object> GetById(long ID);
        Task<object> Get();
    }
}
