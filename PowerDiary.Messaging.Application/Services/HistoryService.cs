using PowerDiary.Messaging.Application.Contracts.Services;
using PowerDiary.Messaging.Application.Models;
using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Application.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly SortedList<DateTime, EventEntry> _events;

        public HistoryService()
        {
            _events = new SortedList<DateTime, EventEntry>();
        }

        public void RecordEntering(Participant participant, DateTime at)
        {
            RecordEvent(new EventContext()
            {
                EventType = Constants.EventType.EnterTheRoom,
                Participant = participant,
                OccurredAt = at
            });
        }

        public void RecordLeaving(Participant participant, DateTime at)
        {
            RecordEvent(new EventContext()
            {
                EventType = Constants.EventType.LeaveTheRoom,
                Participant = participant,
                OccurredAt = at
            });
        }

        public void RecordComment(Participant participant, string comment, DateTime at)
        {
            RecordEvent(new EventContext()
            {
                EventType = Constants.EventType.Comment,
                OccurredAt = at,
                Comment = comment,
                Participant = participant,
            });
        }

        public void RecordHighFive(Participant from, Participant to, DateTime at)
        {
            RecordEvent(new EventContext()
            {
                EventType = Constants.EventType.HighFive,
                OccurredAt = at,
                Participant = from,
                OtherParticipant = to
            });
        }

        public IReadOnlyCollection<EventEntry> GetEvents()
        {
            return _events.Values.ToList();
        }

        private void RecordEvent(EventContext context)
        {
            _events.Add(context.OccurredAt, new EventEntry(context));
        }
    }
}