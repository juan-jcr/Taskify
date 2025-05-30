using Application.TaskList.DTO;
using MediatR;

namespace Application.TaskList.Commands.CreateTask;

public class CreateTaskCommand : IRequest<TaskDto>
{
   public string Title { get; set; } = string.Empty;
   public string? Description { get; set; }
        
   public DateTime? DateOfCreation { get; set; }
}
