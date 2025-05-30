using Application.TaskList.Commands.CreateTask;
using Application.TaskList.Commands.UpdateTask;
using Application.TaskList.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;

internal class TaskProfile : Profile
{
   public TaskProfile()
   {
      CreateMap<TaskEntity, TaskDto>();
      CreateMap<CreateTaskCommand, TaskEntity>();
      CreateMap<UpdateTaskCommand, TaskEntity>()
         .ForMember(dest => dest.Id, opt => opt.Ignore());

   }  
}
