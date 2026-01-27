using Azure.Messaging.ServiceBus;
using Messaging.AzureServiceBus.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Shared.Contracts.Events;

namespace Messaging.Extensions
{
    public static class MessagingServiceCollectionExtensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration config)
        {
            // Azure Service Bus client
            config.GetSection(Properties.CacheSection);
            var connectionString = config.GetConnectionString("ServiceBus");
            var client = new ServiceBusClient(connectionString);

            // Register producer
            services.AddSingleton<IEventPublisher>(sp =>
                new EventPublisher(client));

            // Register consumer hosted service
            //services.AddHostedService<IEventPublisher>();

            return services;
        }
    }
}
