using Application.Common.DTOs.TaskDTO;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.TaskList.Commands.CreateTask
{
    internal class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskDto>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateTaskHandler(
            ITaskRepository taskRepository,
            IMapper mapper,
            ICurrentUserService currentUserService
            )
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = _mapper.Map<TaskEntity>(request);
            //Obtaining the UserId from the authenticated user's claims
            task.UserId = Convert.ToInt32(_currentUserService.UserId);
            await _taskRepository.AddAsync(task);

            return _mapper.Map<TaskDto>(task);
        }

    }

}
