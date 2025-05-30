using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;
internal class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
   public void Configure(EntityTypeBuilder<TaskEntity> builder)
   {
      builder.ToTable("tasks");
      builder.HasKey(t => t.Id);
      builder.Property(t => t.Id).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("id");
      builder.Property(t => t.Title).IsRequired().HasMaxLength(100).HasColumnName("title");
      builder.Property(t => t.Description).HasMaxLength(300).HasColumnName("description");
      builder.Property(t => t.DateOfCreation).HasColumnName("date_creation");
      builder.Property(t => t.Completed).HasColumnName("completed");

      builder.HasOne(t => t.UerEntity)
         .WithMany(u => u.TaskEntities)
         .HasForeignKey(t => t.UserId);
                
   }
}

