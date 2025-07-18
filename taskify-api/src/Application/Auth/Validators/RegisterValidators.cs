using Application.Auth.Commands.RegisterUser;
using FluentValidation;

namespace Application.Auth.Validators;

public class RegisterValidators : AbstractValidator<RegisterUserCommand>
{
   public RegisterValidators()
   {
      RuleFor(register => register.Name)
         .NotEmpty().WithMessage("Name is required");
      
      RuleFor(register => register.Email)
         .NotEmpty().WithMessage("Email is required")
         .EmailAddress().WithMessage("The email is not in a valid format.");


      RuleFor(register => register.Password)
         .NotEmpty().WithMessage("Password is required")
         .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
   }
}
