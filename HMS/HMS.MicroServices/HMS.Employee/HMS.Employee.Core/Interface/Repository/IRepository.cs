namespace HMS.Employee.Core.Interface.Repository
{
    public interface IRepository<T> where T : class
    {
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public Task<T> Delete(T ID);
        public Task<List<T>> Get();
        public Task<T> GetById(Guid ID);
    }
}
