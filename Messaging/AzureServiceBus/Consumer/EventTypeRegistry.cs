using Shared.Contracts.Events;

namespace Messaging.AzureServiceBus.Consumer
{
    public static class EventTypeRegistry
    {
        private static readonly Dictionary<string, Type> _types = new()
    {
        { nameof(EvtSubject.UserRegistered), typeof(UserRegisteredEvent) },
        //{ nameof(PasswordRecoverRequestedEvent), typeof(PasswordRecoverRequestedEvent) }
    };

        public static Type Resolve(string eventName)
        {
            if (!_types.TryGetValue(eventName, out var type))
            {
                throw new InvalidOperationException(
                    $"Unknown integration event: {eventName}");
            }

            return type;
        }
    }

}
