using PowerDiary.Messaging.Application.Controllers;
using PowerDiary.Messaging.Application.Factories;
using PowerDiary.Messaging.Application.Services;
using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var room = new ChatRoom();
            var controller = new ChatRoomController(room, new HistoryService(new EventFactory()));

            var bob = Participant.Create("Bob");
            var kate = Participant.Create("Kate");

            var now = DateTime.UtcNow;

            controller.AddParticipant(bob, now);
            controller.AddParticipant(kate, now.AddMinutes(3));

            controller.AcceptComment(bob, "Hey, Kate - high five?", now.AddMinutes(6));
            controller.AcceptHighFive(kate, bob, now.AddMinutes(7));

            controller.RemoveParticipant(bob, now.AddMinutes(8));
            controller.AcceptComment(kate, "Oh, typical", now.AddMinutes(9));
            controller.RemoveParticipant(kate, now.AddMinutes(10));

            controller.DisplayEvents();

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