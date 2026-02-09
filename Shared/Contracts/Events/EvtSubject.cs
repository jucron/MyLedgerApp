namespace Shared.Contracts.Events
{
    public class EvtSubject
    {
        private EvtSubject(string subject)
        {
            Desc = subject;
        }
        public string Desc {  get; private set; }
        public static EvtSubject UserRegistered => new("user_registered");
        public static EvtSubject PassRecovery => new("pass_recovery");
        public static EvtSubject PassChanged => new("pass_changed");

    }
}
