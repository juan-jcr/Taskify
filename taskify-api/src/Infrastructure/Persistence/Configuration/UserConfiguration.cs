using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("id");
            builder.Property(t => t.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
            builder.Property(t => t.Email).HasMaxLength(100).HasColumnName("email");
            builder.HasIndex(t => t.Email).IsUnique();
            builder.Property(t => t.Password).HasColumnName("password");
            
            
            builder.HasMany(u => u.TaskEntities)
                .WithOne(t => t.UerEntity)
                .HasForeignKey(t => t.UserId);
        }
    }
}
