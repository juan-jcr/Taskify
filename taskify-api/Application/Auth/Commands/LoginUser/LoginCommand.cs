using MediatR;

namespace Application.Auth.Commands.LoginUser
{
    public record LoginCommand(string Email, string Password) : IRequest<string>;
}