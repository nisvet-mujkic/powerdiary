using PowerDiary.Messaging.Application.Contracts.Strategies;
using PowerDiary.Messaging.Application.Models;

namespace PowerDiary.Messaging.Application.Strategies
{
    public class HourlyAggregation : IAggregation
    {
        public void Display(IEnumerable<EventEntry> events)
        {
            foreach (var eventsGroup in new HourlyEvents(events))
            {
                Console.WriteLine(eventsGroup);

                foreach (var @event in new EventsByType(eventsGroup.Events))
                {
                    Console.WriteLine(@event);
                }
            }
        }
    }
}