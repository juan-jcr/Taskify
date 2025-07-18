﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.TaskList.DTO;
using AutoMapper;
using MediatR;

namespace Application.TaskList.Commands.UpdateTask;

public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, TaskDto>
{
   private readonly ITaskRepository _taskRepository;
   private readonly IMapper _mapper;
   private readonly ICurrentUserService _currentUserService;

   public UpdateTaskHandler(
      ITaskRepository taskRepository,
      IMapper mapper,
      ICurrentUserService currentUserService)
   {
      _taskRepository = taskRepository;
      _mapper = mapper;
      _currentUserService = currentUserService;
   }

   public async Task<TaskDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
   {
      var task = await _taskRepository.GetByIdAsync(request.Id);
      if (task == null) throw new ResourceNotFoundException($"Task with id {request.Id} not found.");
            
      _mapper.Map(request, task);
      //Obtaining the UserId of the authenticated user's claims
      task.UserId = Convert.ToInt32(_currentUserService.UserId);
      await _taskRepository.UpdateAsync(task);
      return _mapper.Map<TaskDto>(task);
   }
}

