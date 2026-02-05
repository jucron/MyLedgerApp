using Shared.Contracts.Events;
using Shared.Contracts.Events.Publishable;

namespace Messaging.AzureServiceBus.Consumer
{
    public static class EventTypeRegistry
    {
        private static readonly Dictionary<string, Type> _types = [];

        public static void Register<T>(EvtSubject subject) where T : IPublishableEvent
        {
            _types[subject.Desc] = typeof(T);
        }
        public static Type Resolve(string subject)
        {
            if (!_types.TryGetValue(subject, out var type))
            {
                throw new InvalidOperationException(
                    $"Unknown integration event: {subject}");
            }

            return type;
        }
    }

}
