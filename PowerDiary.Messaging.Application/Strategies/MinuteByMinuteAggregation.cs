using PowerDiary.Messaging.Application.Contracts.Strategies;
using PowerDiary.Messaging.Application.Models;
using System.IO;

namespace PowerDiary.Messaging.Application.Strategies
{
    public class MinuteByMinuteAggregation : IAggregation
    {
        public void Render(IEnumerable<EventEntry> events)
        {
            static string BuildMessage(EventEntry @event)
            {
                var at = @event.Context.OccurredAt.ToShortTimeString();
                var participant = @event.Context.Participant.Name;

                return @event.Context.EventType switch
                {
                    Constants.EventType.EnterTheRoom => $"{at}: {@event.Context.Participant} enters the room",
                    Constants.EventType.LeaveTheRoom => $"{at}: {@event.Context.Participant} leaves the room",
                    Constants.EventType.Comment => $"{at}: {@event.Context.Participant} comments: \"{@event.Context.Comment}\"",
                    Constants.EventType.HighFive => $"{at}: {@event.Context.Participant} high-fives {@event.Context.OtherParticipant}"
                };
            }

            foreach (var @event in events)
            {
                Console.WriteLine(BuildMessage(@event));
            }
        }
    }
}