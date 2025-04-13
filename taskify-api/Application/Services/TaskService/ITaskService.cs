using Application.DTOs.TaskDTO;

namespace Application.Services.TaskService
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> CreateAsync(CreateTaskDto taskDto);
    }
}
