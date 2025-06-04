using Application.TaskList.Validators;
using Application.TaskList.Commands.UpdateTask;
using FluentValidation.TestHelper;
using Xunit;

namespace Application.Tests.TaskList.Validators;
public class UpdateTaskValidatorTests
{
    private readonly UpdateTaskValidator _validator;

    public UpdateTaskValidatorTests()
    {
        _validator = new UpdateTaskValidator();
    }

    
    
    [Fact]
    public void Should_Have_Error_When_Id_Is_Zero()
    {
        var command = new UpdateTaskCommand { Id = 0 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Id must be greater than zero");
    }
    
    [Fact]
    public void Should_Have_Error_When_Title_Is_Empty()
    {
        var command = new UpdateTaskCommand { Id = 1, Title = "" };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Title)
              .WithErrorMessage("Title is required");
    }

    [Fact]
    public void Should_Have_Error_When_Title_Is_Too_Long()
    {
        var command = new UpdateTaskCommand
        {
            Id = 1,
            Title = new string('a', 61)
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Title)
              .WithErrorMessage("The title must be less than 60 characters");
    }

    [Fact]
    public void Should_Have_Error_When_Description_Is_Too_Long()
    {
        var command = new UpdateTaskCommand
        {
            Id = 1,
            Title = "Valid Title",
            Description = new string('a', 151)
        };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Description)
              .WithErrorMessage("The description must be less than 150 characters");
    }

    [Fact]
    public void Should_Not_Have_Errors_When_Command_Is_Valid()
    {
        var command = new UpdateTaskCommand
        {
            Id = 5,
            Title = "Valid Task",
            Description = "Valid description."
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
