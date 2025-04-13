
using Application.DTOs.TaskDTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    internal class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskEntity, TaskDto>();
           
        }  
    }
   
}
