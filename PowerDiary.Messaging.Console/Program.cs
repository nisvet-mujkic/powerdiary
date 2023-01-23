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

            var atTime = new DateTime(2023, 1, 23, 17, 0, 0);

            controller.AddParticipant(bob, atTime);
            controller.AddParticipant(kate, atTime.AddMinutes(3));

            controller.Comment(bob, "Hey, Kate - high five?", atTime.AddMinutes(6));
            controller.HighFive(kate, bob, atTime.AddMinutes(7));

            controller.RemoveParticipant(bob, atTime.AddMinutes(8));
            controller.Comment(kate, "Oh, typical", atTime.AddMinutes(9));
            controller.RemoveParticipant(kate, atTime.AddMinutes(10));

            controller.Display(new MinuteByMinuteDisplayStrategy());

            /*
             * main events: 
             * - enter-the-room
             * - leave-the-room
             * - comment
             * - high-five-another-user
             */

            // maybe use flyweight pattern to avoid many allocations
            //var participants = room.AddParticipants(bob, kate); // dispatch an event 'enter-the-room'



            //room.DisplayEvents("inject object here IDisplaySomething"); // use strategy pattern, minute by minute || houry  

            // use stack or queue for event history, descending chronological order, keep count of events that took place
            // create TimeStamp value object, just to have logic of extracting date separated out
        }
    }
}