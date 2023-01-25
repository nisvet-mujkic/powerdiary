using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Application.Models
{
    public class EventEntry
    {
        public EventEntry(EventContext context)
        {
            Context = context;
        }

        public EventContext Context { get; }
    }

    public class EventContext
    {
        public string EventType { get; set; }

        public DateTime OccurredAt { get; set; }

        public string Comment { get; set; }

        public Participant Participant { get; set; }

        public Participant OtherParticipant { get; set; }
    }
}