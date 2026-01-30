using Azure.Messaging.ServiceBus;
using Messaging.AzureServiceBus;
using Messaging.AzureServiceBus.Consumer;
using Messaging.AzureServiceBus.Producer;
using Microsoft.Extensions.Options;
using Shared;
using Shared.Contracts.Events;

namespace Host.Extensions
{
    public static class MessagingConfig
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration config)
        {

            // Setting singleton prop class
            services.Configure<ServiceBusSettings>(
               config.GetSection(Properties.ServiceBus));

            // Azure Service Bus client
            services.AddSingleton<ServiceBusClient>(sp =>
            {
                var settings = sp
                    .GetRequiredService<IOptions<ServiceBusSettings>>()
                    .Value;

                return new ServiceBusClient(settings.DefaultConnection);
            });

            // Register producer
            services.AddSingleton<IEventPublisher,EventPublisher>();

            // Register consumer hosted service
            services.AddHostedService<ServiceBusEventConsumerHostedService>();

            return services;
        }
    }
}
