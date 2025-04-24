using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Auth.Commands.RegisterUser
{
    internal class RegisterUserHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _password;

        public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher password)
        {
            _userRepository = userRepository;
            _password = password;
        }
        
        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            
            if (existingUser != null)
            {
                throw new AlreadyRegisteredException("Email already registered");
            }
            
            var user = new UserEntity
            {
                Name = request.Name,
                Email = request.Email,
                Password = _password.HashPassword(request.Password)
            };

            await _userRepository.AddAsync(user);
            return "User registered successfully";
        }
    }
}