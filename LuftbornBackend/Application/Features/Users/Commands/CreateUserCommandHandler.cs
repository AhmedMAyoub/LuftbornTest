using Application.Features.Users.Commands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Application.Interfaces.Repository;
using FluentValidation;
using Application.Features.Users.DTOs;
using Application.Features.Users.Validators;
using FluentValidation.Results;
using System.Text;
using System.Security.Cryptography;

namespace Application.Features.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateUserDTO> _validator;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _validator = new CreateUserValidator();
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = _validator.Validate(request.CreateUserDTO);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var hashedPassword = HashPassword(request.CreateUserDTO.Password);

            var user = new User
            {
                Username = request.CreateUserDTO.Username,
                Email = request.CreateUserDTO.Email,
                Password = hashedPassword
            };

            await _userRepository.AddAsync(user);
            return user.Id;
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
