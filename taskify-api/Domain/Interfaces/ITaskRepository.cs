using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskEntity>> GetAllAsync();
        Task AddAsync(TaskEntity taskEntity);
    }
}
