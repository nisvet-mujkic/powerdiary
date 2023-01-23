namespace PowerDiary.Messaging.Domain.Entities
{
    public class ChatRoom
    {
        private readonly HashSet<Participant> _participants;
        public IReadOnlyCollection<Participant> Participants => _participants.ToList();

        public ChatRoom()
        {
            _participants = new HashSet<Participant>();
        }

        public bool AddParticipant(Participant participant) =>
            _participants.Add(participant);

        public bool RemoveParticipant(Participant participant) =>
            _participants.Remove(participant);

        public bool ContainsParticipant(Participant participant) =>
            _participants.Contains(participant);
    }
}