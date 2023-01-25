using PowerDiary.Messaging.Application.Contracts.Strategies;
using PowerDiary.Messaging.Application.Models;
using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Application.Strategies
{
    public class HourlyAggregation : IAggregation
    {
        public void Render(IEnumerable<EventEntry> events)
        {
            var groupedByHour = events.GroupBy(x => x.Context.OccurredAt.Hour,
                (hour, groupedHourly) => new HourGroup(hour, groupedHourly.ToList()));

            foreach (var group in groupedByHour)
            {
                Console.WriteLine(group.Hour);

                var groupedByEventType = group.Events.GroupBy(x => x.Context.EventType,
                    (eventType, events) => new EventGroup(eventType, events.ToList()));

                foreach (var item in groupedByEventType)
                {
                    Console.WriteLine($"\t{BuildMessage(item)}");
                }
            }
        }

        private static string BuildMessage(EventGroup group)
        {
            var eventCount = group.Events.Count;

            return group.EventType switch
            {
                Constants.EventType.EnterTheRoom => $"{eventCount} {Pluralize("participant", "participants", eventCount)} entered",
                Constants.EventType.LeaveTheRoom => $"{eventCount} {Pluralize("participant", "participants", eventCount)} left",
                Constants.EventType.Comment => $"{eventCount} {Pluralize("comment", "comments", eventCount)}",
                Constants.EventType.HighFive => BuildMessageForHighFiveEvent(group),
            };
        }

        private static string BuildMessageForHighFiveEvent(EventGroup eventGroup)
        {
            var events = GetHiveFiveEvents(eventGroup);
            var count = events.Count();
            var highFivesCount = events.Sum(x => x.To.Count);

            return $"{count} {Pluralize("participant", "participants", count)} high-fived {highFivesCount} {Pluralize("participant", "participants", highFivesCount)}";
        }

        private static IEnumerable<HighFivesGroup> GetHiveFiveEvents(EventGroup eventGroup) =>
            eventGroup.Events.GroupBy(x => x.Context.Participant, (from, to) =>
                    new HighFivesGroup(from.Name, to.Select(x => x.Context.OtherParticipant).ToList()));

        private static string Pluralize(string singular, string plural, int count) =>
            count > 1 ? plural : singular;
    }

    public record HourGroup(int Hour, IReadOnlyCollection<EventEntry> Events);

    public record EventGroup(string EventType, IReadOnlyCollection<EventEntry> Events);

    public record HighFivesGroup(string From, IReadOnlyCollection<Participant> To);
}