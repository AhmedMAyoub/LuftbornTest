using Domain.Interfaces;

namespace Domain.Entities
{
    public class Comment : IBaseAuditableEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Content { get; set; } = string.Empty;
        public Guid PostId { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }
        public Post Post { get; set; } = null!;
        public User Author { get; set; } = null!;
    }
}
