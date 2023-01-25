using PowerDiary.Messaging.Application.Extensions;
using System.Collections;

namespace PowerDiary.Messaging.Application.Models
{
    public class HourlyEvents : IEnumerable<HourGroup>
    {
        private readonly IEnumerable<HourGroup> _events;

        public HourlyEvents(IEnumerable<EventEntry> events)
        {
            _events = events.GroupBy(x => x.Context.OccurredAt.Hour, HourGroup.Create);
        }

        public IEnumerator<HourGroup> GetEnumerator() =>
            _events.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    public record HourGroup(TimeOnly Time, IReadOnlyCollection<EventEntry> Events)
    {
        public static HourGroup Create(int Hour, IEnumerable<EventEntry> Events) =>
            new(new TimeOnly(Hour, 0), Events.ToList());

        public override string ToString() =>
            $"{Time:%h tt} ({Events.Count} {"event".Pluralize(Events.Count, "events")})";
    }
}