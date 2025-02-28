using MediatR;

namespace Application.Features.Users.Commands
{
    public class LoginUserCommand : IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public LoginUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
