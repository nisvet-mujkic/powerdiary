using PowerDiary.Messaging.Application.Extensions;
using PowerDiary.Messaging.Domain.Entities;
using System.Collections;

namespace PowerDiary.Messaging.Application.Models
{
    public class EventsByType : IEnumerable<EventGroup>
    {
        private readonly IEnumerable<EventGroup> _events;

        public EventsByType(IEnumerable<EventEntry> events)
        {
            _events = events.GroupBy(x => x.Context.EventType, EventGroup.Create);
        }

        public IEnumerator<EventGroup> GetEnumerator() =>
            _events.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    public record EventGroup(string EventType, IReadOnlyCollection<EventEntry> Events)
    {
        public static EventGroup Create(string EventType, IEnumerable<EventEntry> Events) =>
            new(EventType, Events.ToList());

        public override string ToString() =>
            EventType switch
            {
                Constants.EventType.EnterTheRoom => $"\t- {Events.Count} {"participant".Pluralize(Events.Count, "participants")} entered",
                Constants.EventType.LeaveTheRoom => $"\t- {Events.Count} {"participant".Pluralize(Events.Count, "participants")} left",
                Constants.EventType.Comment => $"\t- {Events.Count} {"comment".Pluralize(Events.Count, "comments")}",
                Constants.EventType.HighFive => BuildMessageForHighFiveEvent(),
            };

        private string BuildMessageForHighFiveEvent()
        {
            var interactions = GetHighFiveInteractions(this);
            var givenCount = interactions.Count();
            var receivedCount = interactions.Sum(x => x.To.Count);

            return $"\t- {givenCount} {"participant".Pluralize(givenCount, "participants")} high-fived {receivedCount} other {"participant".Pluralize(receivedCount, "participants")}";
        }

        private static IEnumerable<HighFivesGroup> GetHighFiveInteractions(EventGroup eventGroup) =>
            eventGroup.Events.GroupBy(x => x.Context.Participant, (from, to) =>
                    new HighFivesGroup(from.Name, to.Select(x => x.Context.OtherParticipant).ToList()));

        private record HighFivesGroup(string From, IReadOnlyCollection<Participant> To);
    }
}