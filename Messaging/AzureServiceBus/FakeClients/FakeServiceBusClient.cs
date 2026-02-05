using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Messaging.AzureServiceBus.FakeClients
{
    public class FakeServiceBusClient : ServiceBusClient
    {
        private const string fakeConnectionStrings = "Endpoint=sb://fake/;SharedAccessKeyName=Fake;SharedAccessKey=Fake";
        private readonly ILogger<FakeServiceBusClient> _log;
        public FakeServiceBusClient(ILogger<FakeServiceBusClient> log) : base(fakeConnectionStrings) 
        { 
            _log = log;
        }

        public override ServiceBusSender CreateSender(string queueName)
        {
            return new FakeServiceBusSender(queueName, _log);
        }
    }

    public class FakeServiceBusSender(string queueName, ILogger<FakeServiceBusClient> log) : ServiceBusSender
    {
        private readonly string _queueName = queueName;
        private readonly ILogger<FakeServiceBusClient> _log = log;

        public override Task SendMessageAsync(ServiceBusMessage message, CancellationToken cancellationToken = default)
        {
            _log.LogInformation($"[Dev] Simulated send to {_queueName}: {message.Body}");
            return Task.CompletedTask;
        }
    }

}
