using Application.TaskList.Commands.CreateTask;
using FluentValidation;

namespace Application.TaskList.Validators
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskValidator()
        { 
            RuleFor(task => task.Id).NotEmpty().WithMessage("Id cannot be null");

            RuleFor(task => task.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(task => task.Title).MaximumLength(60).MinimumLength(2)
                .WithMessage("The title should be between 2 and 60 characters.");

            RuleFor(task => task.Description)
                .MaximumLength(150).MinimumLength(2)
                .WithMessage("The title should be between 2 and 150 characters.");

            
        }
    }
}
