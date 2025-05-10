using Application.TaskList.Commands.CreateTask;
using FluentValidation;

namespace Application.TaskList.Validators
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskValidator( ) 
        {
            RuleFor(task => task.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(task => task.Title).MaximumLength(60).MinimumLength(2)
                .WithMessage("The title should be between 2 and 60 characters.");

            RuleFor(task => task.Description)
                .MaximumLength(150).MinimumLength(2)
                .WithMessage("The title should be between 2 and 150 characters.");

            RuleFor(task => task.DateOfCreation)
                .Must(date => date?.Date >= DateTime.Today)
                .WithMessage("The date must be today or a future date.");

        }
    }
}
