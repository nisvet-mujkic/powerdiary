using FluentAssertions;
using PowerDiary.Messaging.Application;
using PowerDiary.Messaging.Application.Models;
using PowerDiary.Messaging.Application.Services;
using PowerDiary.Messaging.Domain.Entities;
using Xunit;

namespace PowerDiary.Messaging.UnitTests.Services
{
    public class HistoryServiceUnitTests
    {
        private readonly HistoryService _historyService;

        public HistoryServiceUnitTests()
        {
            _historyService = new HistoryService();
        }

        [Fact]
        public void EventsAreStoredAsTheyHappened()
        {
            // Arrange
            var at10am = DateTime.Today.AddHours(10);

            var bob = Participant.Create("Bob");
            var kate = Participant.Create("Kate");

            // Act
            _historyService.RecordEntering(bob, at10am.AddMinutes(15));
            _historyService.RecordEntering(kate, at10am);
            _historyService.RecordLeaving(kate, at10am.AddMinutes(30));
            _historyService.RecordLeaving(bob, at10am.AddMinutes(25));

            // Assert
            var events = _historyService.GetEvents();
            var expected = new List<EventEntry>()
            {
                new(new EventContext() { EventType = Constants.EventType.EnterTheRoom, Participant = kate, OccurredAt = at10am }),
                new(new EventContext() { EventType = Constants.EventType.EnterTheRoom, Participant = bob, OccurredAt = at10am.AddMinutes(15)}),
                new(new EventContext() { EventType = Constants.EventType.LeaveTheRoom, Participant = bob, OccurredAt = at10am.AddMinutes(25)}),
                new(new EventContext() { EventType = Constants.EventType.LeaveTheRoom, Participant = kate, OccurredAt = at10am.AddMinutes(30)}),
            };

            events.Should().HaveCount(expected.Count).And.BeEquivalentTo(expected);
        }
    }
}