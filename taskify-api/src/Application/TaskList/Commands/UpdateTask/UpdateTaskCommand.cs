
using Application.TaskList.DTO;
using MediatR;

namespace Application.TaskList.Commands.UpdateTask;

public class UpdateTaskCommand : IRequest<TaskDto>
{
   public int Id { get; set; }
   public string Title { get; set; } = string.Empty;
   public string? Description { get; set; }
   public DateTime? DateOfCreation { get; set; }
   public bool Completed { get; set; }
}

