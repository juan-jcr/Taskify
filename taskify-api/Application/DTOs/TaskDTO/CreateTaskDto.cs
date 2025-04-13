namespace Application.DTOs.TaskDTO
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
