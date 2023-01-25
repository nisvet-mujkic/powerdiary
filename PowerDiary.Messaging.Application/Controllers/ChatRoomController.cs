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

            _historyService.RecordEntering(participant, at);
        }

        public void RemoveParticipant(Participant participant, DateTime at)
        {
            if (!_room.RemoveParticipant(participant))
                return;

            _historyService.RecordLeaving(participant, at);
        }

        public void PublishComment(Participant participant, string comment, DateTime at)
        {
            if (string.IsNullOrWhiteSpace(comment))
                return;

            if (!_room.ContainsParticipant(participant))
                return;

            _historyService.RecordComment(participant, comment, at);
        }

        public void SendHighFive(Participant from, Participant to, DateTime at)
        {
            if (from == to)
                return;

            if (!_room.ContainsParticipant(from) || !_room.ContainsParticipant(to))
                return;

            _historyService.RecordHighFive(from, to, at);
        }

        public void ViewChatRoomHistory(IAggregation aggregation)
        {
            aggregation.Render(_historyService.GetEvents());
        }
    }
}