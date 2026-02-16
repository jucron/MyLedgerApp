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

            await db.Database.EnsureCreatedAsync();
            //try
            //{
            //    await db.Database.MigrateAsync();
            //    logger.LogInformation("Database migrated successfully");
            //}
            //catch (Exception ex)
            //{
            //    logger.LogWarning(ex, "Database migration failed");
            //}
            //finally
            //{
            //    await db.Database.EnsureCreatedAsync();
            //}
        }
    }

}
