namespace PowerDiary.Messaging.Domain.Entities
{
    public class ChatRoom
    {
        // use HashSet
        private readonly IList<Participant> _participants;
        public IReadOnlyCollection<Participant> Participants => _participants.ToList();

        public ChatRoom()
        {
            _participants = new List<Participant>();
        }

        public void AddParticipant(Participant participant)
        {
            _participants.Add(participant);
        }

        public bool IsInRoom(Participant participant)
        {
            // TODO: rewrite using .Contains()

            foreach (var p in _participants)
                if (p.Name == participant.Name)
                    return true;

            return false;
        }
    }
}
