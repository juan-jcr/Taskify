
using Domain.Entities;
using Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
   {
   }
   public DbSet<TaskEntity> Tareas => Set<TaskEntity>();
   public DbSet<UserEntity> Users => Set<UserEntity>();

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.ApplyConfiguration(new TaskConfiguration());
      modelBuilder.ApplyConfiguration(new UserConfiguration());
   }

}
   
