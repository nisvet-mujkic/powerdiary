using FluentAssertions;
using PowerDiary.Messaging.Domain.Entities;
using Xunit;

namespace PowerDiary.Messaging.UnitTests.Entities
{
    public class ChatRoomUnitTests
    {
        [Fact]
        public void ChatRoomStartsEmpty()
        {
            // Arrange
            var room = new ChatRoom();

            // Assert
            room.Participants.Should().BeEmpty();
        }

        [Fact]
        public void ParticipantsCanJoinTheRoom()
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
        public void ParticipantsCanLeaveTheRoom()
        {
            // Arrange
            var room = new ChatRoom();

            var bob = Participant.Create("Bob");
            var kate = Participant.Create("Kate");

            // Act
            room.AddParticipant(bob);
            room.AddParticipant(kate);

            // Assert
            room.RemoveParticipant(kate).Should().BeTrue();
            room.Participants.Should().HaveCount(1).And.Contain(new List<Participant> { bob });
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
            room.AddParticipant(bob).Should().BeFalse();
            room.ContainsParticipant(bob).Should().BeTrue();
        }

        [Fact]
        public void ParticipantCanRejoinRoomAfterLeaving()
        {
            // Arrange
            var room = new ChatRoom();
            var bob = Participant.Create("Bob");

            // Act
            room.AddParticipant(bob);
            room.RemoveParticipant(bob);

            // Assert
            room.AddParticipant(bob).Should().BeTrue();
            room.ContainsParticipant(bob).Should().BeTrue();
        }
    }
}