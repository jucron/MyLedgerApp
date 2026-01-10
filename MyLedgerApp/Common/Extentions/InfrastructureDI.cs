using MyLedgerApp.Infrastructure.Repositories;

namespace MyLedgerApp.Common.Extentions
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepositoryMock>();
            services.AddScoped<IUserRepository, UserRepositoryMock>();
            services.AddScoped<ILedgerRepository, LedgerRepositoryMock>();
            return services;
        }
    }

}
