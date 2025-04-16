using Application.Common.DTOs.TaskDTO;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.TaskList.Commands.CreateTask;
using AutoMapper;
using MediatR;

namespace Application.TaskList.Commands.UpdateTask
{
    internal class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, TaskDto>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public UpdateTaskHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<TaskDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id);
            if (task == null) throw new ResourceNotFoundException($"Task with id {request.Id} not found.");

            _mapper.Map(request, task);
            await _taskRepository.UpdateAsync(task);
            return _mapper.Map<TaskDto>(task);
        }
    }
}
