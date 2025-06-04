using Application.Auth.Commands.LoginUser;
using FluentValidation;

namespace Application.Auth.Validators;
public class LoginValidators : AbstractValidator<LoginCommand>
{
   public LoginValidators()
   {
      RuleFor(login => login.Password)
         .NotEmpty().WithMessage("Password is required");
      
      RuleFor(login => login.Email)
         .NotEmpty().WithMessage("Email is required")
         .EmailAddress().WithMessage("The email is not in a valid format.");
      
     
   }
}


