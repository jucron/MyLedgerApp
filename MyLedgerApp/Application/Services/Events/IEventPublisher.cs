using Shared.Contracts.Events;

namespace MyLedgerApp.Application.Services.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync(IPublishableEvent @event);
    }
}
