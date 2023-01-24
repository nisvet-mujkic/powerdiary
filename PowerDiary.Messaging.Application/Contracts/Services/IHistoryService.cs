using PowerDiary.Messaging.Application.Services;
using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Application.Contracts.Services
{
    public interface IHistoryService
    {
        void AddEvent(string type, Participant participant, DateTime occurredAt);

        IReadOnlyCollection<EventEntry> GetEvents();
    }
}