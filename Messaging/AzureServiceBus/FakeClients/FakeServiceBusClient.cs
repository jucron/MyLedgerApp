using Azure.Messaging.ServiceBus;
using Messaging.AzureServiceBus.Consumer;
using Microsoft.Extensions.Logging;

namespace Messaging.AzureServiceBus.FakeClients
{
    public class FakeServiceBusClient : ServiceBusClient
    {
        private const string fakeConnectionStrings = "Endpoint=sb://fake/;SharedAccessKeyName=Fake;SharedAccessKey=Fake";
        private readonly ILogger<FakeServiceBusClient> _log;
        private readonly IServiceBusEventDispatcher _dispatcher;

        public FakeServiceBusClient(ILogger<FakeServiceBusClient> log, IServiceBusEventDispatcher dispatcher) : base(fakeConnectionStrings)
        {
            _log = log;
            _dispatcher = dispatcher;
        }

        public override ServiceBusSender CreateSender(string queueName)
        {
            return new FakeServiceBusSender(queueName, _log, _dispatcher);
        }
    }

    public class FakeServiceBusSender(string queueName, ILogger<FakeServiceBusClient> log, IServiceBusEventDispatcher dispatcher) : ServiceBusSender
    {
        private readonly string _queueName = queueName;
        private readonly ILogger<FakeServiceBusClient> _log = log;
        private readonly IServiceBusEventDispatcher _dispatcher = dispatcher;

        public override async Task SendMessageAsync(ServiceBusMessage message, CancellationToken cancellationToken = default)
        {
            _log.LogInformation($"[Dev] Simulated event queued to {_queueName}: {message.Body}");

            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken); //simulate small interval

            var receivedMessage = ServiceBusModelFactory.ServiceBusReceivedMessage(
                    body: message.Body,
                    subject: message.Subject,
                    messageId: message.MessageId,
                    properties: message.ApplicationProperties);

            await _dispatcher.DispatchAsync(receivedMessage);

        }
    }

}
