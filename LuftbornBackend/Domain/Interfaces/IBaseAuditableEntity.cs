
namespace Domain.Interfaces
{
    public interface IBaseAuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
