﻿using Application.Common.Interfaces;
using Application.TaskList.DTO;
using AutoMapper;
using MediatR;

namespace Application.TaskList.Queries.GetAllTasks;

public class GetAllTasksHandler : IRequestHandler<GetAllTaksQuery, IEnumerable<TaskDto>>
{
   private readonly ITaskRepository _taskRepository;
   private readonly IMapper _mapper;

   public GetAllTasksHandler(ITaskRepository taskRepository, IMapper mapper)
   {
      _taskRepository = taskRepository;
      _mapper = mapper;
   }

   public async Task<IEnumerable<TaskDto>> Handle(GetAllTaksQuery request, CancellationToken cancellationToken)
   {
      var task = await _taskRepository.GetAllAsync();
      return _mapper.Map<IEnumerable<TaskDto>>(task);
   }
}
