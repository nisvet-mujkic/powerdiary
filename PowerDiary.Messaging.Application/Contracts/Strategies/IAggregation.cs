using PowerDiary.Messaging.Application.Models;

namespace PowerDiary.Messaging.Application.Contracts.Strategies
{
    public interface IAggregation
    {
        void Display(IEnumerable<EventEntry> events);
    }
}