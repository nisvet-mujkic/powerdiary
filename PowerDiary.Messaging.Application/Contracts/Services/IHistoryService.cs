using PowerDiary.Messaging.Application.Models;
using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Application.Contracts.Services
{
    public interface IHistoryService
    {
        void RecordComment(Participant participant, string comment, DateTime at);

        void RecordEntering(Participant participant, DateTime at);

        void RecordHighFive(Participant from, Participant to, DateTime at);

        void RecordLeaving(Participant participant, DateTime at);

        IReadOnlyCollection<EventEntry> GetEvents();
    }
}