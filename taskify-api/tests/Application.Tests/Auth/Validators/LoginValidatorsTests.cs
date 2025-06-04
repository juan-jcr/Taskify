using Application.Auth.Commands.LoginUser;
using Application.Auth.Validators;
using FluentValidation.TestHelper;

namespace Application.Tests.Auth.Validators;

public class LoginValidatorsTests
{
   private readonly LoginValidators _validator = new();

   [Fact]
   public void Should_Have_Error_When_Email_Is_Empty()
   {
      var command = new LoginCommand("", "password123");
      var result = _validator.TestValidate(command);
      result.ShouldHaveValidationErrorFor(c => c.Email)
         .WithErrorMessage("Email is required");
   }

   [Fact]
   public void Should_Have_Error_When_Email_Is_Invalid()
   {
      var command = new LoginCommand("not-an-email", "password123");
      var result = _validator.TestValidate(command);
      result.ShouldHaveValidationErrorFor(c => c.Email)
         .WithErrorMessage("The email is not in a valid format.");
   }

   [Fact]
   public void Should_Have_Error_When_Password_Is_Empty()
   {
      var command = new LoginCommand("test@example.com", "");
      var result = _validator.TestValidate(command);
      result.ShouldHaveValidationErrorFor(c => c.Password)
         .WithErrorMessage("Password is required");
   }

   [Fact]
   public void Should_Not_Have_Error_When_Command_Is_Valid()
   {
      var command = new LoginCommand("test@example.com", "securePassword123");
      var result = _validator.TestValidate(command);
      result.ShouldNotHaveAnyValidationErrors();
   }
}
