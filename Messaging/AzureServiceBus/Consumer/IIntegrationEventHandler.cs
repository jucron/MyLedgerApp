namespace Messaging.AzureServiceBus.Consumer
{
    public interface IIntegrationEventHandler<TEvent>
    {
        Task HandleAsync(TEvent @event);
    }

}
