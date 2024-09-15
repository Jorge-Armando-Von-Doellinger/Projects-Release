namespace HMS.Employee.Core.Interface.Repository
{
    public interface IRepository<Entity> where Entity : class
    {
        public Task<bool> Add(Entity entity);
        public Task<bool> Update(Entity entity);
        public Task<bool> Delete(Guid ID);
        public Task<List<Entity>> Get();
        public Task<Entity> GetById(Guid ID);
    }
}
