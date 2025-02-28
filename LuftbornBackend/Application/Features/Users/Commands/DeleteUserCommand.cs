using MediatR;

namespace Application.Features.Users.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public DeleteUserCommand(Guid id)
        {
            UserId = id;
        }
    }
}
