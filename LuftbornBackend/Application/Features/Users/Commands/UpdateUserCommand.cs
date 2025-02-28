using MediatR;
using Application.Features.Users.DTOs;

namespace Application.Features.Users.Commands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public UpdateUserDTO UserDTO { get; set; }

        public UpdateUserCommand(Guid userId, UpdateUserDTO updateUserDTO)
        {
            UserId = userId;
            UserDTO = updateUserDTO;
        }
    }
}
