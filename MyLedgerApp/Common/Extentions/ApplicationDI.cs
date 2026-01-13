using MyLedgerApp.Application.Services;
using MyLedgerApp.Application.Services.Auth;
using MyLedgerApp.Application.Services.Transactions;

namespace MyLedgerApp.Common.Extentions
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
    }
}
