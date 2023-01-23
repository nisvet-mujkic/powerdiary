namespace PowerDiary.Messaging.Domain.Entities
{
    public class Participant
    {
        public Participant(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public void Comment(string comment) // make a value object Comment?!
        {

        }

        public void HighFive(Participant participant)
        {

        }

        public void Leave()
        {

        }
    }
}
