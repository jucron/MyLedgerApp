using Azure.Communication.Email;
using Azure.Messaging.ServiceBus;
using Messaging.AzureServiceBus;
using Messaging.AzureServiceBus.Consumer;
using Messaging.AzureServiceBus.Consumer.Handlers;
using Messaging.AzureServiceBus.FakeClients;
using Messaging.AzureServiceBus.Producer;
using Microsoft.Extensions.Options;
using Shared;
using Shared.Contracts.Events;
using Shared.Contracts.Events.Publishable;

namespace Host.Extensions
{
    public static class MessagingDI
    {
        public static IServiceCollection AddMessagingServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
        {

            // Setting singleton prop class
            services.Configure<ServiceBusSettings>(
               config.GetSection(PropertySection.ServiceBus));

            // Azure Service Bus client
            RegisterAzureServiceBusClient(services, !env.IsProduction());

            // Azure Service Email client
            RegisterAzureServiceEmailClient(services, !env.IsProduction());
           

            // Register services
            services.AddSingleton<IEventPublisher,EventPublisher>();
            services.AddSingleton<IServiceBusEventDispatcher, ServiceBusEventDispatcher>();

            // Register event types
            EventTypeRegistry.Register<UserRegisteredEvent>(EvtSubject.UserRegistered);
            EventTypeRegistry.Register<PasswordRecoverRequestedEvent>(EvtSubject.PassRecovery);
            EventTypeRegistry.Register<PasswordChangedEvent>(EvtSubject.PassChanged);

            // Register event handlers
            services.AddScoped<IIntegrationEventHandler<UserRegisteredEvent>, UserRegisteredEmailHandler>();
            services.AddScoped<IIntegrationEventHandler<PasswordRecoverRequestedEvent>, PasswordRecoverRequestedHandler>();
            services.AddScoped<IIntegrationEventHandler<PasswordChangedEvent>, PasswordChangedHandler>();

            // Register consumer hosted service
            services.AddHostedService<ServiceBusEventConsumerHostedService>();

            return services;
        }

        private static void RegisterAzureServiceEmailClient(IServiceCollection services, bool shouldFake)
        {
            if (shouldFake)
            {
                services.AddSingleton<EmailClient, FakeServiceEmailClient>();
            }
            else
            {
                services.AddSingleton<EmailClient>(sp =>
                {
                    var settings = sp
                        .GetRequiredService<IOptions<ServiceBusSettings>>()
                        .Value;

                    return new EmailClient(settings.DefaultConnection);
                });
            }
        }

        private static void RegisterAzureServiceBusClient(IServiceCollection services, bool shouldFake)
        {
            if (shouldFake)
            {
                services.AddSingleton<ServiceBusClient, FakeServiceBusClient>();
            }
            else
            {
                services.AddSingleton<ServiceBusClient>(sp =>
                {
                    var settings = sp
                        .GetRequiredService<IOptions<ServiceBusSettings>>()
                        .Value;

                    return new ServiceBusClient(settings.DefaultConnection);
                });
            }
        }
     
    }
}
