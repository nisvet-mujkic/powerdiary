using PowerDiary.Messaging.Application.Contracts.Factories;
using PowerDiary.Messaging.Application.Contracts.Services;
using PowerDiary.Messaging.Domain.Entities;
using PowerDiary.Messaging.Domain.Events;

namespace PowerDiary.Messaging.Application.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly List<EventEntry> _events;
        private readonly IEventFactory _eventFactory;

        public HistoryService(IEventFactory eventFactory)
        {
            _events = new List<EventEntry>();
            _eventFactory = eventFactory;
        }

        public void AddEvent(string type, Participant participant, DateTime occurredAt)
        {
            var eventType = _eventFactory.GetEvent(type);

            _events.Add(new EventEntry(eventType, occurredAt));
        }

        public IReadOnlyCollection<EventEntry> GetEvents()
        {
            return _events.ToList();
        }
    }

    public abstract class Dummy
    {
        public abstract string Printable { get; }
    }

    public class Enters : Dummy
    {
        private readonly Participant _participant;
        private readonly string _at;

        public Enters(Participant participant, DateTime at)
        {
            _participant = participant;
            _at = at.ToShortTimeString();
        }

        public override string Printable => $"{_at}: {_participant} enter the room";
    }


    public class EventEntry
    {
        public EventEntry(IEvent @event, DateTime occurredAt)
        {
            Event = @event;
            OccurredAt = occurredAt;
        }

        public IEvent Event { get; }

        public DateTime OccurredAt { get; }
    }
}