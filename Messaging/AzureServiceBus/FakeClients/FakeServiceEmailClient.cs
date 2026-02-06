using System.Text;
using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Logging;

namespace Messaging.AzureServiceBus.FakeClients
{
    public class FakeServiceEmailClient : EmailClient
    {
        private readonly ILogger<FakeServiceEmailClient> _log;

        public FakeServiceEmailClient(ILogger<FakeServiceEmailClient> log)
        {
            _log = log;
        }

        public override Task<EmailSendOperation> SendAsync(WaitUntil wait, EmailMessage message, CancellationToken cancellationToken = default)
        {
            _log.LogInformation(BuildEmailLog(message));
            return Task.FromResult(new EmailSendOperation("fake-id", this));
        }

        private static string BuildEmailLog(EmailMessage email)
        {
            StringBuilder sb = new();
            sb.AppendLine("[Dev] simulated Email sent:");
            sb.AppendLine("From: " + string.Join(",", email.SenderAddress));
            sb.AppendLine("To: " + string.Join(",", email.Recipients.To.Select(t=>t.Address)));
            sb.AppendLine("Subject: " + email.Content.Subject);
            sb.AppendLine("Body: " + email.Content.PlainText);
            return sb.ToString();
        }
    }
}
