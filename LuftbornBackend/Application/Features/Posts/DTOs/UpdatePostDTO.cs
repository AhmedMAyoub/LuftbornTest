namespace Application.Features.Posts.DTOs
{
    public class UpdatePostDTO
    {
        public string? Title { get; set; } = default!;
        public string? Content { get; set; } = default!;
    }
}
