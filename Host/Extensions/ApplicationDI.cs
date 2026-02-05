using MyLedgerApp.Application.Properties;
using MyLedgerApp.Application.Services;
using MyLedgerApp.Application.Services.Auth;
using MyLedgerApp.Application.Services.Transactions;
using Shared;

namespace Host.Extensions
{
    public static class ApplicationDI
    {
        /// <summary>
        /// Custom extension in <see cref="IServiceCollection"/>, to register all the application services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILedgerService, LedgerService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }

        /// <summary>
        /// Custom extension in <see cref="IServiceCollection"/>, to register all the application properties as a service.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationProperties(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(
                configuration.GetSection(PropertySection.Jwt));

            services.Configure<CacheSettings>(
                configuration.GetSection(PropertySection.Cache));

            services.AddSingleton<IAppProperties,AppProperties>();
            return services;
        }
    }
}
