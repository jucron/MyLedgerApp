
namespace Shared.Contracts.Events.Publishable
{
    public class PasswordRecoverRequestedEvent() : PublishableEventBase(EvtSubject.PassRecovery)
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string RecoveryToken { get; set; } = null!;
        public DateTime OccurredAt { get; set; }
    }
}
