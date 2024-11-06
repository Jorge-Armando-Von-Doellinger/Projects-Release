namespace HMS.Payments.Core.Entity.Base
{
    public class EntityBase
    {
        public string ID { get; set; }// = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        protected void Update() => UpdatedAt = DateTime.Now;
        public virtual void ValidateEntity()
        {
            //ValidateId();
        }
        private void ValidateId()
        {
            if (string.IsNullOrWhiteSpace(ID)) throw new ArgumentNullException(nameof(ID), "O Identificador do pagamento não pode ser vazio!");
        }
    }
}
