using PowerDiary.Messaging.Application.Controllers;
using PowerDiary.Messaging.Domain.Events;

namespace PowerDiary.Messaging.Application.Contracts.Factories
{
    public interface IEventFactory
    {
        IEvent GetEvent(string type);
    }
}