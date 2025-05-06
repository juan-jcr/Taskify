using System.Text.Json.Serialization;
using Application.Common.Mappings;

namespace Application.Common.DTOs.TaskDTO
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime DateOfCreation { get; set; }
        public bool Completed { get; set; }
    }
}
