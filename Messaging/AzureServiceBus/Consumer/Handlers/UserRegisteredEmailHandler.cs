using Azure.Communication.Email;
using Shared.Contracts.Events.Publishable;

namespace Messaging.AzureServiceBus.Consumer.Handlers
{
    public class UserRegisteredEmailHandler : IIntegrationEventHandler<UserRegisteredEvent>
    {
        private readonly EmailClient _emailClient;

        public UserRegisteredEmailHandler(EmailClient emailClient)
        {
            _emailClient = emailClient;
        }

        public async Task HandleAsync(UserRegisteredEvent @event)
        {
            if (@event?.Email is not null)
                await SendWelcomeEmail(@event.Email);
        }

        private async Task SendWelcomeEmail(string email)
        {
            var emailMessage = new EmailMessage(
                senderAddress: "noreply@myledgerapp.com",
                recipients: new EmailRecipients(
                    new[] { new EmailAddress(email) }),
                content: new EmailContent("Welcome to MyLedgerApp!")
                {
                    PlainText = "Thanks for registering 🎉"
                });

            await _emailClient.SendAsync(Azure.WaitUntil.Started,emailMessage);
        }
    }
}
