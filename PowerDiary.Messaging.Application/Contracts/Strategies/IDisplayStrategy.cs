using PowerDiary.Messaging.Application.Services;

namespace PowerDiary.Messaging.Application.Contracts.Strategies
{
    public interface IDisplayStrategy
    {
        void Display(IEnumerable<EventEntry> events);
    }
}