using PowerDiary.Messaging.Application.Contracts.Strategies;
using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Application.Contracts.Controllers
{
    public interface IChatRoomController
    {
        void AddParticipant(Participant participant, DateTime at);

        void RemoveParticipant(Participant participant, DateTime at);

        void Comment(Participant participant, string comment, DateTime at);

        void HighFive(Participant from, Participant to, DateTime at);

        void Display(IDisplayStrategy strategy);
    }
}
