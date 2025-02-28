using Application.Features.Posts.DTOs;
using MediatR;

namespace Application.Features.Posts.Queries
{
    public class GetAllPostsByUserIdQuery : IRequest<GetAllPostsDTO[]>
    {
        public Guid UserId { get; set; }
        public GetAllPostsByUserIdQuery(Guid id)
        {
            UserId = id;
        }
    }
}
