
namespace Shared.Contracts.Events.Publishable
{
    public class PasswordChangedEvent() : PublishableEventBase(EvtSubject.PassChanged)
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    }
}
