
namespace Messaging.AzureServiceBus
{
    public class ServiceBusSettings
    {
        public string DefaultConnection { get; set; } = null!;
        public string TopicName { get; init; } = null!;

        public string SubscriptionName { get; init; } = null!;
    }
}
