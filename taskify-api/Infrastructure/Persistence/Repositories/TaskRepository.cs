using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;

namespace Infrastructure.Persistence.Repositories
{
    internal class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public TaskRepository(AppDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
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

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            return await _context.Tareas
                .Where(t => t.UserId == Convert.ToInt32(_currentUserService.UserId))
                .ToListAsync();
        }

        public async Task<TaskEntity?> GetByIdAsync(int id)
        {
            var userId = Convert.ToInt32(_currentUserService.UserId);
            return await _context.Tareas
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task UpdateAsync(TaskEntity taskEntity)
        {
            _context.Tareas.Update(taskEntity);
            await _context.SaveChangesAsync();
        }
        
    }
}
