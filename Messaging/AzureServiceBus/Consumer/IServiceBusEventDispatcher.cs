using Azure.Messaging.ServiceBus;

namespace Messaging.AzureServiceBus.Consumer
{
    public interface IServiceBusEventDispatcher
    {
        Task DispatchAsync(ServiceBusReceivedMessage message);
    }
}
