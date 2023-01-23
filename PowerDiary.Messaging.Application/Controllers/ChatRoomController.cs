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

        public void AddParticipant(Participant participant, DateTime at)
        {
            if (_room.IsInRoom(participant))
                return;

            _room.AddParticipant(participant);

            var message = $"{at.ToShortTimeString()}: {participant} enters the room";

            _historyService.AddEvent(Constants.EventType.EnterTheRoom, message, at);
        }

        public void RemoveParticipant(Participant participant, DateTime at)
        {
            if (!_room.IsInRoom(participant))
                return;

            _room.RemoveParticipant(participant);

            var message = $"{at.ToShortTimeString()}: {participant} leaves the room";

            _historyService.AddEvent(Constants.EventType.LeaveTheRoom, message, at);
        } 

        public void AcceptComment(Participant participant, string comment, DateTime at)
        {
            if (!_room.IsInRoom(participant))
                return;

            var message = $"{at.ToShortTimeString()}: {participant} comments: \"{comment}\"";

            _historyService.AddEvent(Constants.EventType.Comment, message, at);
        }

        public void AcceptHighFive(Participant from, Participant to, DateTime at)
        {
            if (!_room.IsInRoom(from) || !_room.IsInRoom(to))
                return;

            var message = $"{at.ToShortTimeString()}: {from} high-fives {to}";
            _historyService.AddEvent(Constants.EventType.HighFive, message, at);
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
