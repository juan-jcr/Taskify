namespace Domain.Entities;

public class UserEntity
{
   public int Id { get; set; }
   public string Name { get; set; } = string.Empty;
   public string Email { get; set; } = string.Empty;
   public string Password { get; set; } = string.Empty;
   public ICollection<TaskEntity> TaskEntities { get; set; } = new List<TaskEntity>();
}


