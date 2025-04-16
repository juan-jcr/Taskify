
using Application.Common.DTOs.TaskDTO;
using MediatR;

namespace Application.TaskList.Queries.GetAllTasks
{
    public class GetAllTaksQuery : IRequest<IEnumerable<TaskDto>>
    {
    }
}
