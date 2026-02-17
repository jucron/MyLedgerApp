using Microsoft.EntityFrameworkCore;
using MyLedgerApp.Application.Properties;
using MyLedgerApp.Infrastructure.DbConfig;

namespace Host.Extensions
{
    public static class DatabaseStartup
    {
        public static async Task BootDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            var prop = scope.ServiceProvider.GetRequiredService<IAppProperties>();

            await db.Database.EnsureCreatedAsync();

            if (prop.DbSettings.ShouldMigrate)
                await MigrateDb();
           
            if (prop.DbSettings.ShouldReset)
                await db.Database.EnsureDeletedAsync();

            await db.Database.EnsureCreatedAsync();

            async Task MigrateDb()
            {
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

}
