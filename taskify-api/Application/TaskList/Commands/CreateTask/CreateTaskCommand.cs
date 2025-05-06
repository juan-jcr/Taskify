
using System.Text.Json.Serialization;
using Application.Common.DTOs.TaskDTO;
using Application.Common.Mappings;
using MediatR;

namespace Application.TaskList.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<TaskDto>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime DateOfCreation { get; set; }
    }
}
