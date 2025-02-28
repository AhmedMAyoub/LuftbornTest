using Application.Features.Users.DTOs;
using MediatR;

namespace Application.Features.Users.Queries
{
    public class GetCurrentUserQuery : IRequest<UserDTO>
    {
        public Guid UserId { get; set; }

        public GetCurrentUserQuery(Guid id)
        {
            UserId = id;
        }

    }
}
