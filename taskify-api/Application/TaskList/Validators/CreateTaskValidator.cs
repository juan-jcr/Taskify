using Application.TaskList.Commands.CreateTask;
using FluentValidation;

namespace Application.TaskList.Validators
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskValidator( ) 
        {
            RuleFor(task => task.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(task => task.Title).MaximumLength(60)
                .WithMessage("The title must not exceed 60 characters.");

            RuleFor(task => task.Description)
                .MaximumLength(150)
                .WithMessage("The description must not exceed 150 characters.");

        }
    }
}
