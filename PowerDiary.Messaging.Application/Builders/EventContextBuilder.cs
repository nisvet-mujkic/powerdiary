using PowerDiary.Messaging.Application.Models;
using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Application.Services
{
    public class EventContextBuilder
    {
        private readonly EventContext _eventContext;

        private EventContextBuilder(DateTime occurredAt)
        {
            _eventContext = new()
            {
                OccurredAt = occurredAt
            };
        }

        public static EventContextBuilder New(DateTime occurredAt) => new(occurredAt);

        public EventContextBuilder WithEventType(string eventType)
        {
            _eventContext.EventType = eventType;
            return this;
        }

        public EventContextBuilder WithComment(string comment)
        {
            _eventContext.Comment = comment;
            return this;
        }

        public EventContextBuilder WithParticipant(Participant participant)
        {
            _eventContext.Participant = participant;
            return this;
        }

        public EventContextBuilder WithOtherParticipant(Participant participant)
        {
            _eventContext.OtherParticipant = participant;
            return this;
        }

        public EventContext Build() => _eventContext;
    }
}