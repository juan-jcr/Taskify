using Application.Common.DTOs.TaskDTO;
using MediatR;

namespace Application.TaskList.Queries.GetTaskById
{
    public class GetTaskByIdQuery : IRequest<TaskDto?>
    {
        public int Id { get; set; }

        public GetTaskByIdQuery(int id)
        {
            Id = id;
        }
    }
}
