using Application.TaskList.DTO;
using MediatR;


namespace Application.TaskList.Queries.GetTaskById;

public class GetTaskByIdQuery : IRequest<TaskDto?>
{
   public int Id { get; set; }

   public GetTaskByIdQuery(int id)
   {
      Id = id;
   }
}
