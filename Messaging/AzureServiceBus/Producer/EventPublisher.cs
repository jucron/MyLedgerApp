using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Shared.Contracts.Events;

namespace Messaging.AzureServiceBus.Producer
{
    public class EventPublisher : IEventPublisher
    {
        private readonly ServiceBusSender _sender;

        public EventPublisher(ServiceBusClient client)
        {
            _sender = client.CreateSender("user-events");
        }
        public async Task PublishAsync(IPublishableEvent @event)
        {
            var json = JsonSerializer.Serialize(@event);
            var message = new ServiceBusMessage(json)
            {
                ContentType = "application/json",
                Subject = @event.Subject
            };

            await _sender.SendMessageAsync(message);
        }
    }
}
