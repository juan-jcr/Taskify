using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Auth.Commands.LoginUser
{
    internal class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUtils _utils;

        public LoginHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUtils utils)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _utils = utils;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);


            if (user == null || !_passwordHasher.Verify(request.Password, user.Password))
                throw new InvalidCredentialsException("Invalid email or password");

            return _utils.GenerateToken(user);
        }
    }
}