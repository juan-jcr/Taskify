
using System.Text.Json.Serialization;
using Application.Common.DTOs.TaskDTO;
using Application.Common.Mappings;
using MediatR;

namespace Application.TaskList.Commands.CreateTask
{
    public class UpdateTaskCommand : IRequest<TaskDto>
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public bool Completed { get; set; }
    }
}
