namespace Shared.Contracts.Events
{
    public class UserRegisteredEvent() : PublishableEventBase(EvtSubject.UserRegistered)
    {
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime OccurredAt { get; set; }
    }
}
