using Shared.Contracts.Events.Publishable;

namespace Shared.Contracts.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync(IPublishableEvent @event);
    }
}
