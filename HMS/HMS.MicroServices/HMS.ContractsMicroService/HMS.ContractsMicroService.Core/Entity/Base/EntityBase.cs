namespace HMS.ContractsMicroService.Core.Entity.Base
{
    public class EntityBase
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        protected void Update()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}
