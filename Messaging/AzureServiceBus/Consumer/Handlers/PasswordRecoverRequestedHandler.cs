using Azure.Communication.Email;
using Shared.Contracts.Events.Publishable;

namespace Messaging.AzureServiceBus.Consumer.Handlers
{
    public class PasswordRecoverRequestedHandler : IIntegrationEventHandler<PasswordRecoverRequestedEvent>
    {
        private readonly EmailClient _emailClient;

        public PasswordRecoverRequestedHandler(EmailClient emailClient)
        {
            _emailClient = emailClient;
        }

        public async Task HandleAsync(PasswordRecoverRequestedEvent @event)
        {
            if (@event?.Email is not null)
                await SendPassRecoveryEmail(@event);
        }

        private async Task SendPassRecoveryEmail(PasswordRecoverRequestedEvent evt)
        {
            var emailMessage = new EmailMessage(
                senderAddress: "noreply@myledgerapp.com",
                recipients: new EmailRecipients(
                    new[] { new EmailAddress(evt.Email) }),
                content: new EmailContent("This is your details to reset your password!")
                {
                    PlainText = $"- username: {evt.Username}\n - recoveryToken: {evt.RecoveryToken}",
                });

            await _emailClient.SendAsync(Azure.WaitUntil.Started,emailMessage);
        }
    }
}
