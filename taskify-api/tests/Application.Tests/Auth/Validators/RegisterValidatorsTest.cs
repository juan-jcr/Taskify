using Application.Auth.Commands.RegisterUser;
using Application.Auth.Validators;
using FluentValidation.TestHelper;

namespace Application.Tests.Auth.Validators;

public class RegisterValidatorsTests
{
   private readonly RegisterValidators _validator = new();

   [Fact]
   public void Should_Have_Error_When_Name_Is_Empty()
   {
      var command = new RegisterUserCommand("", "test@example.com", "secret");
      var result = _validator.TestValidate(command);
      result.ShouldHaveValidationErrorFor(c => c.Name)
         .WithErrorMessage("Name is required");
   }

   [Fact]
   public void Should_Have_Error_When_Email_Is_Invalid()
   {
      var command = new RegisterUserCommand("John", "bad-email", "secret");
      var result = _validator.TestValidate(command);
      result.ShouldHaveValidationErrorFor(c => c.Email)
         .WithErrorMessage("The email is not in a valid format.");
   }

   [Fact]
   public void Should_Have_Error_When_Password_Too_Short()
   {
      var command = new RegisterUserCommand("John", "john@example.com", "123");
      var result = _validator.TestValidate(command);
      result.ShouldHaveValidationErrorFor(c => c.Password)
         .WithErrorMessage("Password must be at least 6 characters long");
   }

   [Fact]
   public void Should_Not_Have_Errors_When_Command_Is_Valid()
   {
      var command = new RegisterUserCommand("John", "john@example.com", "StrongPass123");
      var result = _validator.TestValidate(command);
      result.ShouldNotHaveAnyValidationErrors();
   }
}
