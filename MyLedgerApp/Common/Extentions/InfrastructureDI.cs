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
            services.AddScoped<ITransactionRepository, TransactionRepositoryMock>();
            services.AddScoped<IUserRepository, UserRepositoryMock>();
            services.AddScoped<ILedgerRepository, LedgerRepositoryMock>();
            return services;
        }
    }

}
