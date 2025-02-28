using MediatR;
using Application.Features.Posts.DTOs;
using Application.Interfaces.Repository;

namespace Application.Features.Posts.Queries
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, GetAllPostsDTO[]>
    {
        private readonly IPostRepository _postRepository;
        public GetAllPostsQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<GetAllPostsDTO[]> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetAllAsync();

            return posts.Select(p => new GetAllPostsDTO
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                Author = p.Author != null ? p.Author.Username : "Unknown",
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToArray();
        }
    }
}
