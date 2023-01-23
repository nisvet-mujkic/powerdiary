using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Application.Contracts.Controllers
{
    public interface IChatRoomController
    {
        void AddParticipant(Participant participant);
    }
}
