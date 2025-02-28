using Application.Features.Users.DTOs;
using Application.Interfaces.Repository;
using MediatR;

namespace Application.Features.Users.Queries
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;

        public GetCurrentUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
            };
        }
    }
}
