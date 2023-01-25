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

            var bob = Participant.Create("Bob");
            var kate = Participant.Create("Kate");

            // Act
            room.AddParticipant(bob);
            room.AddParticipant(kate);

            // Assert
            room.Participants.Should().HaveCount(2).And.Contain(new List<Participant> { bob, kate });
        }

        [Fact]
        public void ParticipantCantJoinRoomIfHeAlreadyJoined()
        {
            // Arrange
            var room = new ChatRoom();
            var bob = Participant.Create("Bob");

            // Act
            room.AddParticipant(bob);

            // Assert
            room.ContainsParticipant(bob).Should().BeTrue();
        }


    }
}