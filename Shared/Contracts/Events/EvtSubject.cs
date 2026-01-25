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

    }
}
