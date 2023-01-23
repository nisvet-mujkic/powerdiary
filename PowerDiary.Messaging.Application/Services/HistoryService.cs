using PowerDiary.Messaging.Application.Contracts.Factories;
using PowerDiary.Messaging.Application.Contracts.Services;
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

        public void AddEvent(string type, string message, DateTime occurredAt)
        {
            var eventType = _eventFactory.GetEvent(type);

            _events.Add(new EventEntry(eventType, message, occurredAt));
        }

        public IReadOnlyCollection<EventEntry> GetEvents()
        {
            return _events.ToList();
        }
    }

    public class EventEntry
    {
        public EventEntry(IEvent @event, string message, DateTime occurredAt)
        {
            Event = @event;
            Message = message;
            OccurredAt = occurredAt;
        }

        public IEvent Event { get; }

        public string Message { get; }

        public DateTime OccurredAt { get; }
    }
}