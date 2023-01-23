using PowerDiary.Messaging.Application.Contracts.Controllers;
using PowerDiary.Messaging.Application.Contracts.Services;
using PowerDiary.Messaging.Domain.Entities;
using PowerDiary.Messaging.Domain.Events;

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

            //var logMessage = BuildMessage().When().ParticipantEntersRoom(participant).Build();
            _historyService.AddEvent("enter-the-room", $"{participant} enters the room");

            //var eventType = _eventsFactory.GetEvent(eventType);
        }

        public void DisplayEvents()
        {
            var events = _historyService.GetEvents();

            foreach (var entry in events)
            {
                Console.WriteLine(entry.Message);
            }
        }
    }
}
