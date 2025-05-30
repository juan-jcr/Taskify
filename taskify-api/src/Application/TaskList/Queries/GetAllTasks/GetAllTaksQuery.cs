using Application.TaskList.DTO;
using MediatR;

namespace Application.TaskList.Queries.GetAllTasks;
public class GetAllTaksQuery : IRequest<IEnumerable<TaskDto>>
{
}

