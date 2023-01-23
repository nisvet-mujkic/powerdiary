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
            var room = new ChatRoom();
            var controller = new ChatRoomController(room, null);
            
            // Act
            var bob = new Participant("Bob");
            controller.AddParticipant(bob);

            // Assert
            room.Participants.Should().HaveCount(1);
        }

        [Fact]
        public void ParticipantCantJoinTheSameRoom()
        {
            // Arrange
            var room = new ChatRoom();
            var controller = new ChatRoomController(room, null);

            // Act
            var bob = new Participant("Bob");
            controller.AddParticipant(bob);
            controller.AddParticipant(bob);

            // Assert
            room.Participants.Should().HaveCount(1);
        }
    }
}