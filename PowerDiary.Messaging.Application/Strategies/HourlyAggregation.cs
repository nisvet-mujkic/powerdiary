using PowerDiary.Messaging.Application.Contracts.Strategies;
using PowerDiary.Messaging.Application.Models;

namespace PowerDiary.Messaging.Application.Strategies
{
    public class HourlyAggregation : IAggregation
    {
        public void Render(IEnumerable<EventEntry> events)
        {
            //var grouping = events.GroupBy(x => x.OccurredAt.Hour);

            //foreach (var group in grouping)
            //{
            //    Console.WriteLine(group.Key);

            //    foreach (var item in group)
            //    {
            //    }
            //}
        }
    }
}