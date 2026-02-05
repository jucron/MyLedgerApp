using Shared.Contracts.Events.Publishable;

namespace Messaging.AzureServiceBus.Consumer
{
    public static class EventTypeRegistry
    {
        private static readonly Dictionary<string, Type> _types = [];

        public static void Register<T>(string subject) where T : IPublishableEvent
        {
            _types[subject] = typeof(T);
        }
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
