﻿namespace Application.DTOs.TaskDTO
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DateOfCreation { get; set; }
        public bool Completed { get; set; }
    }
}
