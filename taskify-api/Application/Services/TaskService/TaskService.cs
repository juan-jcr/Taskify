using Application.DTOs.TaskDTO;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
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

        public async Task<TaskDto> CreateAsync(CreateTaskDto taskDto)
        {
            var task = _mapper.Map<TaskEntity>(taskDto);
            await _taskRepository.AddAsync(task);
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<TaskDto> GetByIdAsync(int id)
        {
            var task = await GetById(id);
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var task = await _taskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskDto>>(task);
        }

        public async Task<TaskDto> UpdateAsync(int id, UpdateTaskDto dto)
        {
            var task = await GetById(id);
            _mapper.Map(dto, task);
            await _taskRepository.UpdateAsync(task);
            return _mapper.Map<TaskDto>(task);
        }

        public async Task DeleteAsync(int id)
        {
           var task = await GetById(id);
           await _taskRepository.DeleteAsync(task);
        }

        private async  Task<TaskEntity> GetById(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) throw new ResourceNotFoundException($"Task with id {id} not found.");
            return task;

        }
    }
}
