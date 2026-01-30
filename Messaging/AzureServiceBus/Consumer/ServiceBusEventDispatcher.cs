using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Messaging.AzureServiceBus.Consumer
{
    public sealed class ServiceBusEventDispatcher(IServiceScopeFactory scopeFactory, ILogger<ServiceBusEventDispatcher> logger) : IServiceBusEventDispatcher
    {
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private readonly ILogger<ServiceBusEventDispatcher> _logger = logger;

        public async Task DispatchAsync(ServiceBusReceivedMessage message)
        {
            var eventTypeName = message.Subject;

            var eventType = EventTypeRegistry.Resolve(eventTypeName);

            using var scope = _scopeFactory.CreateScope();
            var provider = scope.ServiceProvider;

            try
            {
                var evt = JsonSerializer.Deserialize(message.Body, eventType);
                if (evt is null)
                    return;

                var handlerType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                var handlers = provider.GetServices(handlerType);

                foreach (dynamic? handler in handlers)
                {
                    await handler?.HandleAsync((dynamic)evt);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error dispatching message {MessageId}", message.MessageId);
            }
            
        }
    }

}
