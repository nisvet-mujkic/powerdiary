using PowerDiary.Messaging.Application.Contracts.Controllers;
using PowerDiary.Messaging.Application.Contracts.Services;
using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Application.Controllers
{
    public class ChatRoomController : IChatRoomController
    {
        private readonly ChatRoom _room;
        private readonly IHistoryService _historyService;

        public ChatRoomController(ChatRoom room, IHistoryService historyService)
        {
            _room = room;
            _historyService = historyService;
        }

        public void AddParticipant(Participant participant)
        {
            if (_room.IsInRoom(participant))
                return;

            _room.AddParticipant(participant);

            //var logMessage = BuildMessage();
            _historyService.AddEvent("enter-the-room", "Bob enters the room");

            //var eventType = _eventsFactory.GetEvent(eventType);
        }
    }

    public interface IEvent
    {

    }

    public class ParticipantEntersTheRoom : IEvent
    {

    }
}
