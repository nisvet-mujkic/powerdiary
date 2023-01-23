using FluentAssertions;
using PowerDiary.Messaging.Domain.Entities;
using Xunit;

namespace PowerDiary.Messaging.UnitTests.Entities
{
    public class ChatRoomUnitTests
    {
        [Fact]
        public void ChatRoomIsEmptyAtFirst()
        {
            // Arrange
            var room = new ChatRoom();

            // Assert
            room.Participants.Should().BeEmpty();
        }

        [Fact]
        public void ChatRoomCanHaveParticipants()
        {
            // Arrange
            var room = new ChatRoom();

            var bob = new Participant("Bob");
            var kate = new Participant("Kate");

            // Act
            room.AddParticipant(bob);
            room.AddParticipant(kate);

            // Assert
            room.Participants.Should().HaveCount(2).And.Contain(new List<Participant> { bob, kate });
        }

        [Fact]
        public void ChatRoomDetectsAlreadyJoinedMembers()
        {
            // Arrange
            var room = new ChatRoom();
            var bob = new Participant("Bob");

            room.AddParticipant(bob);

            // Assert
            room.IsInRoom(bob).Should().BeTrue();
        }
    }
}