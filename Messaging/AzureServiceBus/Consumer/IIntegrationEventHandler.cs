using Shared.Contracts.Events.Publishable;

namespace Messaging.AzureServiceBus.Consumer
{
    public interface IIntegrationEventHandler<TEvent> where TEvent: IPublishableEvent
    {
        Task HandleAsync(TEvent @event);
    }

}
