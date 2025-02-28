using Application.Interfaces;
using Application.Interfaces.Repository;
using MediatR;
using System.Text;
using System.Security.Cryptography;

namespace Application.Features.Users.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;

        public LoginUserCommandHandler(IUserRepository userRepository, ITokenProvider tokenProvider)
        {
            _userRepository = userRepository;
            _tokenProvider = tokenProvider;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByUsername(request.Username);
            // print to console
            Console.WriteLine("User: " + user.Username);
            if (user == null) return null;

            using var sha256 = SHA256.Create();
            var hashedPassword = HashPassword(request.Password);
            // print the password to console
            Console.WriteLine("Password: " + hashedPassword);
            if (user.Password != hashedPassword) return null;

            return _tokenProvider.GenerateToken(user);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
