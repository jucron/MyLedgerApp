namespace Shared.Contracts.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync(IPublishableEvent @event);
    }
}
