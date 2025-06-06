using Application.TaskList.Commands.UpdateTask;
using FluentValidation;

namespace Application.TaskList.Validators;

public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
{
   public UpdateTaskValidator()
   { 
      RuleFor(task => task.Id)
         .GreaterThan(0)
         .WithMessage("Id must be greater than zero");


      RuleFor(task => task.Title).NotEmpty().WithMessage("Title is required");
      RuleFor(task => task.Title).MaximumLength(60).WithMessage("The title must be less than 60 characters");

      RuleFor(task => task.Description)
         .MaximumLength(150)
         .WithMessage("The description must be less than 150 characters");
      RuleFor(task => task.DateOfCreation).NotNull().NotEmpty().WithMessage("Date of creation is required");

            
   }
}
