using PowerDiary.Messaging.Application.Contracts.Controllers;
using PowerDiary.Messaging.Application.Controllers;
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
            IChatRoomController controller = new ChatRoomController(room, new HistoryService());

            var bob = Participant.Create("Bob");
            var kate = Participant.Create("Kate");
            var john = Participant.Create("John");

            var at = new DateTime(2023, 1, 23, 13, 0, 0);

            controller.AddParticipant(bob, at.AddMinutes(3));
            controller.AddParticipant(john, at.AddMinutes(55));
            controller.AddParticipant(kate, at.AddMinutes(63));

            controller.PublishComment(bob, "Hey, Kate - high five?", at.AddMinutes(76));
            controller.SendHighFive(kate, bob, at.AddMinutes(87));
            controller.SendHighFive(bob, kate, at.AddMinutes(88));

            controller.PublishComment(john, "Hey, Kate - will you high five me as well?", at.AddMinutes(89));
            controller.SendHighFive(kate, john, at.AddMinutes(96));

            controller.RemoveParticipant(bob, at.AddMinutes(98));
            controller.PublishComment(kate, "Oh, typical", at.AddMinutes(100));
            controller.RemoveParticipant(kate, at.AddMinutes(135));
            controller.RemoveParticipant(john, at.AddHours(3));

            controller.ViewChatRoomHistory(new MinuteByMinuteAggregation());

            System.Console.WriteLine();

            controller.ViewChatRoomHistory(new HourlyAggregation());
        }
    }
}