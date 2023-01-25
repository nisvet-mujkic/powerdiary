using PowerDiary.Messaging.Application.Contracts.Strategies;
using PowerDiary.Messaging.Application.Models;
using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Application.Strategies
{
    public class HourlyAggregation : IAggregation
    {
        public void Render(IEnumerable<EventEntry> events)
        {
            var grouping = events.GroupBy(x => x.Context.OccurredAt.Hour,
                (hour, groupedHourly) => new HourGroup(hour, groupedHourly.ToList()));

            foreach (var group in grouping)
            {
                Console.WriteLine(group.Hour);

                var other = group.Events.GroupBy(x => x.Context.EventType,
                    (eventType, events) => new EventGroup(eventType, events.ToList()));

                foreach (var item in other)
                {
                    Console.WriteLine($"\t{Message(item)}");
                }
            }
        }

        private static string Message(EventGroup group)
        {
            var eventCount = group.Events.Count;

            var xxx = string.Empty;

            if (group.EventType == Constants.EventType.HighFive)
            {
                var x = group.Events.GroupBy(x => x.Context.Participant, (from, to) => new HighFivesGroup(from.Name, to.Select(x => x.Context.OtherParticipant).ToList()));

                var c = x.Count();
                xxx = $"{c} {PluralizeWord("participant", "participants", c)} high-fived {x.Sum(x => x.To.Count)} {PluralizeWord("participant", "participants", x.Sum(x => x.To.Count))}";
            }

            return group.EventType switch
            {
                Constants.EventType.EnterTheRoom => $"{eventCount} {PluralizeWord("participant", "participants", eventCount)} entered",
                Constants.EventType.LeaveTheRoom => $"{eventCount} {PluralizeWord("participant", "participants", eventCount)} left",
                Constants.EventType.Comment => $"{eventCount} {PluralizeWord("comment", "comments", eventCount)}",
                Constants.EventType.HighFive => xxx
            };
        }

        // create class
        private static string PluralizeWord(string singular, string plural, int count) =>
            count > 1 ? plural : singular;
    }

    public record HourGroup(int Hour, IReadOnlyCollection<EventEntry> Events);

    public record EventGroup(string EventType, IReadOnlyCollection<EventEntry> Events);

    public record HighFivesGroup(string from, IReadOnlyCollection<Participant> To);
}