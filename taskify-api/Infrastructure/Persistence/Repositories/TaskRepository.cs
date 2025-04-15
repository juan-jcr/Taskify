
using Domain.Entities;
using Application.Interfaces;
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

        public async Task DeleteAsync(TaskEntity taskEntity)
        {
            _context.Tareas.Remove(taskEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync() => await _context.Tareas.ToListAsync();

        public async Task<TaskEntity?> GetByIdAsync(int id)
        {
            return await _context.Tareas.FindAsync(id);
        }

        public async Task UpdateAsync(TaskEntity taskEntity)
        {
            _context.Tareas.Update(taskEntity);
            await _context.SaveChangesAsync();
        }
        
    }
}
