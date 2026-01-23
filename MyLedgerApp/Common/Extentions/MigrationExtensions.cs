using Microsoft.EntityFrameworkCore;
using MyLedgerApp.Infrastructure.DbConfig;

namespace MyLedgerApp.Common.Extentions
{
    public static class MigrationExtensions
    {
        public static async Task TryMigrateAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                await db.Database.MigrateAsync();
                logger.LogInformation("Database migrated successfully");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Database migration failed");
            }
        }
    }

}
