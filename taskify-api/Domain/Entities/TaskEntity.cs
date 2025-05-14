namespace Domain.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public bool Completed { get; set; }

        // Navigation properties
        public UserEntity UerEntity{ get; set; } = null!;
        public int UserId { get; set; } 

    }
}
