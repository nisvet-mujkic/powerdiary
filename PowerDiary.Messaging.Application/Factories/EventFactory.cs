using PowerDiary.Messaging.Application.Contracts.Factories;
using PowerDiary.Messaging.Domain.Events;

namespace PowerDiary.Messaging.Application.Factories
{
    public class EventFactory : IEventFactory
    {
        private readonly Dictionary<string, Type> _events = new Dictionary<string, Type>()
        {
            { "enter-the-room", typeof(EnterRoom) },
            { "leave-the-room", typeof(LeaveRoom) },
            { "comment", typeof(Comment) },
            { "high-five-another-user", typeof(HighFive) },
        };

        public IEvent GetEvent(string type)
        {
            var eventExists = _events.TryGetValue(type, out Type eventType);
            var @event = eventExists ? eventType : null;

            return (IEvent)Activator.CreateInstance(@event);
        }
    }
}
