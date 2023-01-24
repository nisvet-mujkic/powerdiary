using PowerDiary.Messaging.Application.Contracts.Controllers;
using PowerDiary.Messaging.Application.Contracts.Services;
using PowerDiary.Messaging.Application.Contracts.Strategies;
using PowerDiary.Messaging.Application.Services;
using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Application.Controllers
{
    // TODO: ne svidja mi se history service i nacin na koji dodajem poruke, treba mi neku builder
    // prikazivanje po satu ce biti fun fun, iskoristiti dva puta group by
    // test, test, test

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

            //var message = $"{at.ToShortTimeString()}: {participant} enters the room";
            _historyService.AddEvent(Constants.EventType.EnterTheRoom, participant, at);
        }

        public void RemoveParticipant(Participant participant, DateTime at)
        {
            if (!_room.RemoveParticipant(participant))
                return;

            var message = $"{at.ToShortTimeString()}: {participant} leaves the room";
            _historyService.AddEvent(Constants.EventType.LeaveTheRoom, participant, at);
        }

        public void PublishComment(Participant participant, string comment, DateTime at)
        {
            if (!_room.ContainsParticipant(participant))
                return;

            var message = $"{at.ToShortTimeString()}: {participant} comments: \"{comment}\"";
            _historyService.AddEvent(Constants.EventType.Comment, participant, at);
        }

        public void SendHighFive(Participant from, Participant to, DateTime at)
        {
            if (!_room.ContainsParticipant(from) || !_room.ContainsParticipant(to))
                return;

            var message = $"{at.ToShortTimeString()}: {from} high-fives {to}";
            _historyService.AddEvent(Constants.EventType.HighFive, from, at);
        }

        public void Display(IDisplayStrategy strategy)
        {
            var events = _historyService.GetEvents();

            strategy.Display(events);
        }
    }
}
