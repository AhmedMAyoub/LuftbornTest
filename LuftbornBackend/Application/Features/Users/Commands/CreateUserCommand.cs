using Application.Features.Users.DTOs;
using MediatR;

namespace Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public CreateUserDTO CreateUserDTO { get; set; }

        public CreateUserCommand(CreateUserDTO createUserDTO)
        {
            CreateUserDTO = createUserDTO;
        }
    }
}
