using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extension
{
    public static class MigrationExtension
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>(); 
                dbContext.Database.Migrate();
            }
        }
    }
}
