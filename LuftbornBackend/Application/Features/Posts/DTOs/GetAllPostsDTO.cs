namespace Application.Features.Posts.DTOs
{
    public class GetAllPostsDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string Author { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}