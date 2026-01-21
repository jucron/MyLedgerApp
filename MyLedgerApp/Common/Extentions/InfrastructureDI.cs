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
            services.AddSingleton<ITransactionRepository, TransactionRepositoryMock>();
            services.AddSingleton<IUserRepository, UserRepositoryMock>();
            services.AddSingleton<ILedgerRepository, LedgerRepository>();
            return services;
        }
    }

}
