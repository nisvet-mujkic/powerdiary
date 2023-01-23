using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Application.Contracts.Controllers
{
    public interface IChatRoomController
    {
        void AddParticipant(Participant participant, DateTime at);

        void RemoveParticipant(Participant participant, DateTime at);

        void AcceptComment(Participant participant, string comment, DateTime at);

        void AcceptHighFive(Participant from, Participant to, DateTime at);
    }
}
