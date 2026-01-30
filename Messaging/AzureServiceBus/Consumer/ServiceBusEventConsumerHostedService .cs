using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;

namespace Messaging.AzureServiceBus.Consumer
{
    public sealed class ServiceBusEventConsumerHostedService : BackgroundService
    {
        private readonly ServiceBusProcessor _processor;
        private readonly IServiceBusEventDispatcher _dispatcher;

        public ServiceBusEventConsumerHostedService(
            ServiceBusClient client,
            IServiceBusEventDispatcher dispatcher,
            ServiceBusSettings options)
        {
            _dispatcher = dispatcher;
            _processor = client.CreateProcessor(options.TopicName, options.SubscriptionName);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _processor.ProcessMessageAsync += async args =>
            {
                await _dispatcher.DispatchAsync(args.Message);
                await args.CompleteMessageAsync(args.Message);
            };

            _processor.ProcessErrorAsync += _ => Task.CompletedTask;

            await _processor.StartProcessingAsync(stoppingToken);
        }
    }

}
