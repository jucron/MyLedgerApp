namespace Shared.Contracts.Events
{
    public abstract class PublishableEventBase: IPublishableEvent
    {
        protected EvtSubject _subject;

        protected PublishableEventBase(EvtSubject subject)
        {
            _subject = subject ?? throw new ArgumentNullException(nameof(subject));
        }
        public string Subject => _subject.Desc;
    }
}
