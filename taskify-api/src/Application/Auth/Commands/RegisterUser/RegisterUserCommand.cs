using MediatR;

namespace Application.Auth.Commands.RegisterUser;

public record RegisterUserCommand(string Name, string Email, string Password) : IRequest<string> { }
