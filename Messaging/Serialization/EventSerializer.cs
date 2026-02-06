using System.Text.Json;
using Shared.Contracts.Events.Publishable;

namespace Messaging.Serialization
{
    public static class EventSerializer
    {
        public static BinaryData SerializeEvent(this IPublishableEvent evt)
        {
            var bytes = JsonSerializer.SerializeToUtf8Bytes(
                evt,
                evt.GetType());

            return new BinaryData(bytes);
        }

        public static object? DeserializeEvent(this BinaryData binaryData, Type eventType)
        {
            var stream = binaryData.ToStream();
            return JsonSerializer.Deserialize(stream, eventType);
        }
    }
}
