using Application.DTOs.TaskDTO;


namespace Application.Services.TaskService
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> CreateAsync(CreateTaskDto taskDto);
        Task<TaskDto> UpdateAsync(int id, UpdateTaskDto dto);
        Task<TaskDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
