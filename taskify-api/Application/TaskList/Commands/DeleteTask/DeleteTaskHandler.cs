using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.TaskList.Commands.DeleteTask
{
    internal class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand>
    {

        private readonly ITaskRepository _taskRepository;

        public DeleteTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id);
            if (task == null) 
            {
                throw new ResourceNotFoundException("Task Not found");
            }
            await _taskRepository.DeleteAsync(task);

        }
    }
}
