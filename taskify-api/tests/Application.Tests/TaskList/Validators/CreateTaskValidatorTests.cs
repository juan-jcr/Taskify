
using Application.TaskList.Commands.CreateTask;
using Application.TaskList.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.Tests.TaskList.Validators;

public class CreateTaskValidatorTests
{
   private readonly CreateTaskValidator _validator;

   public CreateTaskValidatorTests()
   {
      _validator = new CreateTaskValidator();
   }

   [Fact]
   public void Should_Have_Error_When_Title_Is_Empty()
   {
      var command = new CreateTaskCommand { Title = "" };
      var result = _validator.TestValidate(command);
      result.ShouldHaveValidationErrorFor(c => c.Title)
         .WithErrorMessage("Title is required");
   }

   [Fact]
   public void Should_Have_Error_When_Title_Exceeds_Max_Length()
   {
      var command = new CreateTaskCommand { Title = new string('a', 61) };
      var result = _validator.TestValidate(command);
      result.ShouldHaveValidationErrorFor(c => c.Title)
         .WithErrorMessage("The title must not exceed 60 characters.");
   }

   [Fact]
   public void Should_Have_Error_When_Description_Exceeds_Max_Length()
   {
      var command = new CreateTaskCommand { Title = "Valid Title", Description = new string('a', 151) };
      var result = _validator.TestValidate(command);
      result.ShouldHaveValidationErrorFor(c => c.Description)
         .WithErrorMessage("The description must not exceed 150 characters.");
   }

   [Fact]
   public void Should_Not_Have_Errors_When_Valid()
   {
      var command = new CreateTaskCommand
      {
         Title = "Valid Task",
         Description = "This is a valid description."
      };

      var result = _validator.TestValidate(command);
      result.ShouldNotHaveAnyValidationErrors();
   }
}
