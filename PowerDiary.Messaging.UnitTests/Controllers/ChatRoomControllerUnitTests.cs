using FluentAssertions;
using Moq;
using PowerDiary.Messaging.Application.Contracts.Services;
using PowerDiary.Messaging.Application.Controllers;
using PowerDiary.Messaging.Domain.Entities;
using Xunit;

namespace PowerDiary.Messaging.UnitTests.Controllers
{
    public class ChatRoomControllerUnitTests
    {
        private readonly Mock<IHistoryService> _mockHistoryService;

        public ChatRoomControllerUnitTests()
        {
            _mockHistoryService = new Mock<IHistoryService>();
        }

        [Fact]
        public void ParticipantCanJoinTheRoom()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var room = new ChatRoom();
            var controller = new ChatRoomController(room, _mockHistoryService.Object);

            // Act
            var bob = Participant.Create("Bob");
            controller.AddParticipant(bob, now);

            // Assert
            _mockHistoryService.Verify(x => x.RecordEntering(bob, now), Times.Once());
            room.Participants.Should().HaveCount(1);
        }

        [Fact]
        public void ParticipantCantJoinTheSameRoom()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var room = new ChatRoom();
            var controller = new ChatRoomController(room, _mockHistoryService.Object);

            // Act
            var bob = Participant.Create("Bob");
            controller.AddParticipant(bob, now);
            controller.AddParticipant(bob, now);

            // Assert
            _mockHistoryService.Verify(x => x.RecordEntering(bob, now), Times.Once());
            room.Participants.Should().HaveCount(1);
        }

        [Fact]
        public void ParticipantCanLeaveTheRoom()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var room = new ChatRoom();
            var controller = new ChatRoomController(room, _mockHistoryService.Object);

            var bob = Participant.Create("Bob");
            controller.AddParticipant(bob, now);

            // Act
            controller.RemoveParticipant(bob, now);

            // Assert
            _mockHistoryService.Verify(x => x.RecordLeaving(bob, now), Times.Once());
            room.Participants.Should().HaveCount(0);
        }

        [Fact]
        public void ParticipantCanComment()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var room = new ChatRoom();
            var controller = new ChatRoomController(room, _mockHistoryService.Object);

            var bob = Participant.Create("Bob");
            controller.AddParticipant(bob, now);

            // Act
            controller.PublishComment(bob, "This is a comment", now.AddMinutes(3));

            // Assert
            _mockHistoryService.Verify(
                x => x.RecordComment(bob, "This is a comment", now.AddMinutes(3)), Times.Once());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ParticipantCantLeaveAnEmptyComment(string comment)
        {
            // Arrange
            var now = DateTime.UtcNow;
            var room = new ChatRoom();
            var controller = new ChatRoomController(room, _mockHistoryService.Object);

            var bob = Participant.Create("Bob");
            controller.AddParticipant(bob, now);

            // Act
            controller.PublishComment(bob, comment, now.AddMinutes(3));

            // Assert
            _mockHistoryService.Verify(
                x => x.RecordComment(bob, comment, now.AddMinutes(3)), Times.Never());
        }

        [Fact]
        public void ParticipantCanGiveHighFiveToOtherUser()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var room = new ChatRoom();
            var controller = new ChatRoomController(room, _mockHistoryService.Object);

            var bob = Participant.Create("Bob");
            var kate = Participant.Create("Kate");
            controller.AddParticipant(bob, now);
            controller.AddParticipant(kate, now.AddMinutes(3));

            // Act
            controller.SendHighFive(kate, bob, now.AddMinutes(5));

            // Assert
            _mockHistoryService.Verify(
                x => x.RecordHighFive(kate, bob, now.AddMinutes(5)), Times.Once());
        }

        [Fact]
        public void ParticipantCanGiveHighFiveToThemselves()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var room = new ChatRoom();
            var controller = new ChatRoomController(room, _mockHistoryService.Object);

            var bob = Participant.Create("Bob");
            controller.AddParticipant(bob, now);

            // Act
            controller.SendHighFive(bob, bob, now.AddMinutes(5));

            // Assert
            _mockHistoryService.Verify(
                x => x.RecordHighFive(bob, bob, now.AddMinutes(5)), Times.Never());
        }
    }
}