using FluentAssertions;
using PowerDiary.Messaging.Application.Controllers;
using PowerDiary.Messaging.Domain.Entities;
using Xunit;

namespace PowerDiary.Messaging.UnitTests.Controllers
{
    public class ChatRoomControllerUnitTests
    {
        [Fact]
        public void ParticipantCanJoinTheRoom()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var room = new ChatRoom();
            var controller = new ChatRoomController(room, null);
            
            // Act
            var bob = Participant.Create("Bob");
            controller.AddParticipant(bob, now);

            // Assert
            room.Participants.Should().HaveCount(1);
        }

        [Fact]
        public void ParticipantCantJoinTheSameRoom()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var room = new ChatRoom();
            var controller = new ChatRoomController(room, null);

            // Act
            var bob = Participant.Create("Bob");
            controller.AddParticipant(bob, now);
            controller.AddParticipant(bob, now);

            // Assert
            room.Participants.Should().HaveCount(1);
        }
    }
}