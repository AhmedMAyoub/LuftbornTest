using Application.Features.Posts.DTOs;
using MediatR;

namespace Application.Features.Posts.Queries
{
    public class GetAllPostsQuery : IRequest<GetAllPostsDTO[]>
    {
        public GetAllPostsQuery() { }
    }
}
