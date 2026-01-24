using MyLedgerApp.Infrastructure.DbSessions;
using MyLedgerApp.Infrastructure.Repositories;

namespace MyLedgerApp.Common.Extentions
{
    public static class InfrastructureDI
    {
        /// <summary>
        /// Custom extension in <see cref="IServiceCollection"/>, to register all the infrastructure services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILedgerRepository, LedgerRepository>();

            services.AddScoped<IDbSession, DbSession>();

            return services;
        }
    }

}
