using Azure.Messaging.ServiceBus;
using Messaging.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Contracts.Events;
using Shared.Contracts.Events.Publishable;

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
                var json = @event.SerializeEvent();
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
