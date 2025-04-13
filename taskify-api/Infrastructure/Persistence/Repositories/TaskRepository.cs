﻿
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskEntity taskEntity)
        {
            _context.Tareas.Add(taskEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync() => await _context.Tareas.ToListAsync();
 
    }
}
