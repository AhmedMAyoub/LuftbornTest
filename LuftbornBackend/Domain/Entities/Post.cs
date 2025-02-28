using Domain.Interfaces;

namespace Domain.Entities
{
    public class Post : IBaseAuditableEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
        public User Author { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public Post(string title, string content, Guid authorId)
        {
            Id = Guid.NewGuid();
            Title = title;
            Content = content;
            AuthorId = authorId;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        private Post() { }
    }
}
