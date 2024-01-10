using System.ComponentModel.DataAnnotations;

namespace ISSA.Contract.Repository.Entity
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedTime = DateTime.UtcNow;
        }

        [Key]
        public string Id { get; set; }
        public bool IsDelete { get; set; } = false;
        public DateTime CreatedTime { get; set; }
        public DateTime? LastUpdatedTime { get; set; }

        //public DateTimeOffset? DeletedTime { get; set; }
    }
}
