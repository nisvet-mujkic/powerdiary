using PowerDiary.Messaging.Application.Contracts.Controllers;
using PowerDiary.Messaging.Application.Contracts.Services;
using PowerDiary.Messaging.Application.Contracts.Strategies;
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

        public void AddParticipant(Participant participant, DateTime at)
        {
            if (!_room.AddParticipant(participant))
                return;

            var message = $"{at.ToShortTimeString()}: {participant} enters the room";
            _historyService.AddEvent(Constants.EventType.EnterTheRoom, message, at);
        }

        public void RemoveParticipant(Participant participant, DateTime at)
        {
            if (!_room.RemoveParticipant(participant))
                return;

            var message = $"{at.ToShortTimeString()}: {participant} leaves the room";
            _historyService.AddEvent(Constants.EventType.LeaveTheRoom, message, at);
        } 

        public void Comment(Participant participant, string comment, DateTime at)
        {
            if (!_room.ContainsParticipant(participant))
                return;

            var message = $"{at.ToShortTimeString()}: {participant} comments: \"{comment}\"";
            _historyService.AddEvent(Constants.EventType.Comment, message, at);
        }

        public void HighFive(Participant from, Participant to, DateTime at)
        {
            if (!_room.ContainsParticipant(from) || !_room.ContainsParticipant(to))
                return;

            var message = $"{at.ToShortTimeString()}: {from} high-fives {to}";
            _historyService.AddEvent(Constants.EventType.HighFive, message, at);
        }

        public void Display(IDisplayStrategy strategy)
        {
            var events = _historyService.GetEvents();

            strategy.Display(events);
        }
    }
}
