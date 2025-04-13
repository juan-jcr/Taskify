using Application.DTOs.TaskDTO;
using AutoMapper;
using Domain.Interfaces;

namespace Application.Services.TaskService
{
    internal class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var task = await _taskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskDto>>(task);
        }
    }
}
