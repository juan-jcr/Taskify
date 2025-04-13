﻿using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskEntity>> GetAllAsync();
        Task AddAsync(TaskEntity taskEntity);
        Task UpdateAsync(TaskEntity taskEntity);
        Task<TaskEntity?> GetByIdAsync(int id);
        Task DeleteAsync(TaskEntity taskEntity);
    }
}
