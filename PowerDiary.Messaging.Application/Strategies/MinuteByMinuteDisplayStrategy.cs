using PowerDiary.Messaging.Application.Contracts.Strategies;
using PowerDiary.Messaging.Application.Services;

namespace PowerDiary.Messaging.Application.Strategies
{
    public class MinuteByMinuteDisplayStrategy : IDisplayStrategy
    {
        public void Display(IEnumerable<EventEntry> events)
        {
            foreach (var @event in events)
            {
                Console.WriteLine(@event.Message);
            }
        }
    }
}