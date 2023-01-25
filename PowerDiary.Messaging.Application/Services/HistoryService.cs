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
            var context = EventContextBuilder.New(at)
                    .WithEventType(Constants.EventType.EnterTheRoom)
                    .WithParticipant(participant)
                    .Build();

            RecordEvent(context);
        }

        public void RecordLeaving(Participant participant, DateTime at)
        {
            var context = EventContextBuilder.New(at)
                    .WithEventType(Constants.EventType.LeaveTheRoom)
                    .WithParticipant(participant)
                    .Build();

            RecordEvent(context);
        }

        public void RecordComment(Participant participant, string comment, DateTime at)
        {
            var context = EventContextBuilder.New(at)
                    .WithEventType(Constants.EventType.Comment)
                    .WithParticipant(participant)
                    .WithComment(comment)
                    .Build();

            RecordEvent(context);
        }

        public void RecordHighFive(Participant from, Participant to, DateTime at)
        {
            var context = EventContextBuilder.New(at)
                    .WithEventType(Constants.EventType.HighFive)
                    .WithParticipant(from)
                    .WithOtherParticipant(to)
                    .Build();

            RecordEvent(context);
        }

        public IReadOnlyCollection<EventEntry> GetEvents() =>
            _events.Values.ToList();

        private void RecordEvent(EventContext context)
        {
            _events.TryAdd(context.OccurredAt, new EventEntry(context));
        }
    }
}