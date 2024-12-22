namespace HMS.ContractsMicroService.Core.Entity.Base
{
    public class EntityBase
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        protected void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
