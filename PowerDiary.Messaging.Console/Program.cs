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
            var controller = new ChatRoomController(new ChatRoom(), new HistoryService());

            SimulateChatRoomActivity(controller);

            controller.DisplayMinuteByMinuteHistory();

            System.Console.WriteLine();
            Thread.Sleep(1500);

            controller.DisplayHourlyHistory();
        }

        static void SimulateChatRoomActivity(IChatRoomController controller)
        {
            var at = DateTime.Today.AddHours(11);

            var bob = Participant.Create("Bob");
            var kate = Participant.Create("Kate");
            var john = Participant.Create("John");
            var jane = Participant.Create("Jane");
            var chris = Participant.Create("Chris");

            controller.AddParticipant(bob, at.AddMinutes(5));
            controller.AddParticipant(kate, at.AddMinutes(6));

            controller.PublishComment(bob, "Hey, Kate - what's up?", at.AddMinutes(7));
            controller.PublishComment(kate, "Hey, Bob - all good.", at.AddMinutes(9));
            controller.PublishComment(bob, "I'm pretty happy too. Would you high-five me?", at.AddMinutes(10));
            controller.SendHighFive(kate, bob, at.AddMinutes(15));
            controller.PublishComment(bob, "Nice!!!", at.AddMinutes(16));

            controller.PublishComment(kate, "I've talked to John. He'll be here in an hour.", at.AddMinutes(20));
            controller.PublishComment(bob, "Let's wait for him then...", at.AddMinutes(25));

            controller.AddParticipant(john, at.AddHours(1).AddMinutes(25));
            controller.PublishComment(kate, "Hey John!", at.AddHours(1).AddMinutes(30));
            controller.PublishComment(bob, "Hey mate, welcome!", at.AddHours(1).AddMinutes(32));
            controller.SendHighFive(john, kate, at.AddHours(1).AddMinutes(35));
            controller.SendHighFive(john, bob, at.AddHours(1).AddMinutes(35).AddSeconds(30));
            controller.PublishComment(john, "Jane called me few minutes ago. She'll join with Chris in about 2 and a half hours", at.AddHours(1).AddMinutes(38));
            controller.RemoveParticipant(bob, at.AddHours(1).AddMinutes(40));

            controller.AddParticipant(jane, at.AddHours(2).AddMinutes(35));
            controller.AddParticipant(chris, at.AddHours(2).AddMinutes(45));

            controller.SendHighFive(jane, kate, at.AddHours(2).AddMinutes(46));
            controller.SendHighFive(jane, john, at.AddHours(2).AddMinutes(46).AddSeconds(30));
            controller.SendHighFive(chris, kate, at.AddHours(2).AddMinutes(47));
            controller.SendHighFive(chris, john, at.AddHours(2).AddMinutes(47).AddSeconds(30));

            controller.PublishComment(kate, "This has been a blast, but I have to go!", at.AddHours(3).AddMinutes(15));
            controller.RemoveParticipant(kate, at.AddHours(3).AddMinutes(16));

            controller.PublishComment(john, "Bye!", at.AddHours(3).AddMinutes(17));
            controller.PublishComment(jane, "Bye!", at.AddHours(3).AddMinutes(17).AddSeconds(15));
            controller.PublishComment(chris, "Bye!", at.AddHours(3).AddMinutes(17).AddSeconds(25));

            controller.RemoveParticipant(john,  at.AddHours(4).AddMinutes(5));
            controller.RemoveParticipant(jane,  at.AddHours(4).AddMinutes(6));

            controller.PublishComment(chris, "Oh, well... I should leave as well", at.AddHours(4).AddMinutes(10));
            controller.RemoveParticipant(chris, at.AddHours(4).AddMinutes(11));
        }
    }
}