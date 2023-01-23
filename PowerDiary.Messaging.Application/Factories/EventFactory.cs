using PowerDiary.Messaging.Application.Contracts.Factories;
using PowerDiary.Messaging.Application.Controllers;
using PowerDiary.Messaging.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerDiary.Messaging.Application.Factories
{
    public class EventFactory : IEventFactory
    {
        private readonly Dictionary<string, Type> _events = new Dictionary<string, Type>()
        {
            { "enter-the-room", typeof(EnterRoom) }
        };

        public IEvent GetEvent(string type)
        {
            var eventExists = _events.TryGetValue(type, out Type eventType);
            var @event = eventExists ? eventType : null;

            return (IEvent)Activator.CreateInstance(@event);
        }
    }
}
