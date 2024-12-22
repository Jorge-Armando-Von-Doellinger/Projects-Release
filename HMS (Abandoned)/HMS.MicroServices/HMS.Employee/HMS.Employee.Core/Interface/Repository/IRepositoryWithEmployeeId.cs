namespace HMS.Employee.Core.Interface.Repository
{
    public interface IRepositoryWithEmployeeId<Entity> : IRepository<Entity> where Entity : class
    {
        public Task<List<Entity>> GetByEmployeeId(Guid ID);
    }
}
