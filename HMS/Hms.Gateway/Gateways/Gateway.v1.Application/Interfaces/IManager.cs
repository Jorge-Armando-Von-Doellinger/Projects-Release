namespace Gateway.v1.Application.Interfaces
{
    public interface IManager
    {
        Task<object> Add(object input);
        Task<object> Update(object input);
        Task<object> Delete(long ID);
        Task<object> GetById(long ID);
        Task<object> Get();
    }
}
