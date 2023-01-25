using PowerDiary.Messaging.Application.Models;
using PowerDiary.Messaging.Domain.Entities;

namespace PowerDiary.Messaging.Application.Contracts.Services
{
    public interface IHistoryService
    {
        /// <summary>
        /// Records 'comment' event
        /// </summary>
        /// <param name="participant">Participant who left a comment</param>
        /// <param name="comment">Comment that was left by the participant</param>
        /// <param name="at">Datetime when comment was left</param>
        void RecordComment(Participant participant, string comment, DateTime at);

        /// <summary>
        /// Records 'enter-the-room' event
        /// </summary>
        /// <param name="participant">Participant who entered a comment</param>
        /// <param name="at">Datetime when the participant entered the room</param>
        void RecordEntering(Participant participant, DateTime at);

        /// <summary>
        /// Records 'high-five-another-user' event
        /// </summary>
        /// <param name="from">Participant that gives high five</param>
        /// <param name="to">Participant that receives high five</param>
        /// <param name="at">Datetime when the participant high-fived other participant</param>
        void RecordHighFive(Participant from, Participant to, DateTime at);

        /// <summary>
        /// Records 'leave-the-room' event
        /// </summary>
        /// <param name="participant">Participant that left the roon</param>
        /// <param name="at">Datetime when the participant left the room</param>
        void RecordLeaving(Participant participant, DateTime at);

        /// <summary>
        /// Returns sorted list of events
        /// </summary>
        /// <returns>Sorted events</returns>
        IReadOnlyCollection<EventEntry> GetEvents();
    }
}