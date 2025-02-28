using Application.Features.Posts.DTOs;
using Application.Features.Posts.Queries;
using Application.Interfaces.Repository;
using MediatR;
using System;
using System.Threading;


namespace Application.Features.Users.Commands
{
    public class GetPostsByUserIdQueryHandler : IRequestHandler<GetAllPostsByUserIdQuery, GetAllPostsDTO[]>
    {
        private readonly IPostRepository _postRepository;

        public GetPostsByUserIdQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<GetAllPostsDTO[]> Handle(GetAllPostsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetPostsByAuthorIdAsync(request.UserId);

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
