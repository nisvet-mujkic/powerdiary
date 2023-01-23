using PowerDiary.Messaging.Application.Controllers;
using PowerDiary.Messaging.Application.Services;

namespace PowerDiary.Messaging.Application.Contracts.Services
{
    public interface IHistoryService
    {
        void AddEvent(string type, string message, DateTime occurredAt);

        IReadOnlyCollection<EventEntry> GetEvents();
    }
}