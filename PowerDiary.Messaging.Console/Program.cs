using PowerDiary.Messaging.Application.Contracts.Controllers;
using PowerDiary.Messaging.Application.Controllers;
using PowerDiary.Messaging.Application.Factories;
using PowerDiary.Messaging.Application.Services;
using PowerDiary.Messaging.Application.Strategies;
using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var room = new ChatRoom();
            IChatRoomController controller = new ChatRoomController(room, new HistoryService(new EventFactory()));

            var bob = Participant.Create("Bob");
            var kate = Participant.Create("Kate");

            var at = new DateTime(2023, 1, 23, 13, 0, 0);

            controller.AddParticipant(bob, at);
            controller.AddParticipant(kate, at.AddMinutes(3));

            controller.PublishComment(bob, "Hey, Kate - high five?", at.AddMinutes(6));
            controller.SendHighFive(kate, bob, at.AddMinutes(7));

            controller.RemoveParticipant(bob, at.AddMinutes(8));
            controller.PublishComment(kate, "Oh, typical", at.AddMinutes(9));
            controller.RemoveParticipant(kate, at.AddMinutes(10));

            controller.Display(new MinuteByMinuteDisplayStrategy());
        }
    }
}