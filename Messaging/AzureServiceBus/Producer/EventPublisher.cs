using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Contracts.Events;

namespace Messaging.AzureServiceBus.Producer
{
    public class EventPublisher : IEventPublisher
    {
        private readonly ServiceBusSender _sender;
        private readonly ILogger<EventPublisher> _logger;

        public EventPublisher(ServiceBusClient client, IOptions<ServiceBusSettings> options,  ILogger<EventPublisher> logger)
        {
            var topicName = options?.Value?.TopicName
                            ?? throw new ArgumentNullException(nameof(ServiceBusSettings));

            _sender = client.CreateSender(topicName);
            _logger = logger;
        }
        public async Task PublishAsync(IPublishableEvent @event)
        {
            try
            {
                var json = JsonSerializer.Serialize(@event);
                var message = new ServiceBusMessage(json)
                {
                    ContentType = "application/json",
                    Subject = @event.Subject
                };

                await _sender.SendMessageAsync(message);

            } 
            catch 
            {
            _logger
                .LogError("Error publishing event of type {EventType} with subject {EventSubject}", @event.GetType().Name, @event.Subject);
            }
            
        }
    }
}
