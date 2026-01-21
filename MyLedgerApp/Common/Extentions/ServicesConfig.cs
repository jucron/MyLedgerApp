using Microsoft.EntityFrameworkCore;
using MyLedgerApp.Application.Documentation;
using MyLedgerApp.Application.Services.Auth;
using MyLedgerApp.Infrastructure.DbConfig;


namespace MyLedgerApp.Common.Extentions
{
    public static class ServicesConfig
    {
        /// <summary>
        /// Custom extension in <see cref="IServiceCollection"/>, to configure App's Authentication Configuration.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthConfig(this IServiceCollection services, ConfigurationManager configuration)
        {
            AuthConfig.ConfigureAuth(services, configuration.GetJwtSettings());
            return services;
        }

        /// <summary>
        /// Custom extension in <see cref="IServiceCollection"/>, to configure App's Swagger Configuration.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services, ConfigurationManager configuration)
        {
            var apiVersion = configuration.GetAppSettings().ApiVersion;
            services.AddSwaggerGen(c => SwaggerConfig.ConfigSwaggerOptions(apiVersion, c));
            return services;
        }

        /// <summary>
        /// Custom extension in <see cref="IServiceCollection"/>, to configure App's Database Configuration.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabaseConfig(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetDbSettings().DefaultConnection));

            return services;
        }
    }
}
