namespace HMS.Employee.Core.Interface.Manager
{
    public interface IManager<Response>
    {
        Task<Response> Add(object input);
        Task<Response> Update(object updateInput);
        Task<Response> DeleteById(Guid ID);
        Task<Response> Get();
        Task<Response> GetById(Guid ID);
    }
    public interface IManager<Response, Input>
    {
        Task<Response> Add(Input input);
        Task<Response> Update(Input updateInput);
        Task<Response> DeleteById(Guid ID);
        Task<Response> Get();
        Task<Response> GetById(Guid ID);
    }
}
