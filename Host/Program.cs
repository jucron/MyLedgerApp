using System.Text.Json.Serialization;
using Host.Extensions;
using MyLedgerApp.Application.Middlewares;

namespace Host
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;
            var configuration = builder.Configuration;
            var env = builder.Environment;

            // Controllers
            services.AddControllers()
                .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerConfig(configuration);

            // App Services
            services.AddApplicationProperties(configuration);
            services.AddApplicationServices();
            services.AddInfrastructureServices();
            services.AddMessagingServices(configuration, env);

            services.AddAuthConfig(configuration);

            services.AddDatabaseConfig(configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                await app.BootDatabaseAsync();

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ApplicationMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
