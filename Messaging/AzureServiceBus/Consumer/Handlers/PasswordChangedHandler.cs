using Azure.Communication.Email;
using Shared.Contracts.Events.Publishable;

namespace Messaging.AzureServiceBus.Consumer.Handlers
{
    public class PasswordChangedHandler : IIntegrationEventHandler<PasswordChangedEvent>
    {
        private readonly EmailClient _emailClient;

        public PasswordChangedHandler(EmailClient emailClient)
        {
            _emailClient = emailClient;
        }

        public async Task HandleAsync(PasswordChangedEvent @event)
        {
            if (@event?.Email is not null)
                await SendPassChangedEmail(@event);
        }

        private async Task SendPassChangedEmail(PasswordChangedEvent evt)
        {
            var emailMessage = new EmailMessage(
                senderAddress: "noreply@myledgerapp.com",
                recipients: new EmailRecipients(
                    new[] { new EmailAddress(evt.Email) }),
                content: new EmailContent("Your password have been changed!")
                {
                    PlainText = $"Dear {evt.Username}," +
                    $"\nyour password have been changed at {evt.OccurredAt}. " +
                    $"\nIf you don't recognize this change, please send us an email right away!",
                });

            await _emailClient.SendAsync(Azure.WaitUntil.Started,emailMessage);
        }
    }
}
