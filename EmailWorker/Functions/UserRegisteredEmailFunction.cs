using System.Text.Json;
using Azure.Communication.Email;
using Microsoft.Azure.WebJobs;
using Shared.Contracts.Events;

namespace EmailWorker.Functions
{
    public class UserRegisteredEmailFunction
    {
        private readonly EmailClient _emailClient;

        public UserRegisteredEmailFunction(EmailClient emailClient)
        {
            _emailClient = emailClient;
        }

        [FunctionName("UserRegisteredEmail")]
        public async Task Run(
            [ServiceBusTrigger("user-events", Connection = "ServiceBus")]
        string message)
        {
            var evt = JsonSerializer.Deserialize<UserRegisteredEvent>(message);

            if (evt?.Email != null)
                await SendWelcomeEmail(evt.Email);
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
