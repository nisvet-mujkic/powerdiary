namespace PowerDiary.Messaging.Domain.Entities
{
    public record Participant(string Name)
    {
        public static Participant Create(string name) =>
            new Participant(name);

        public override string ToString() => Name;
    }
}