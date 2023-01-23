namespace PowerDiary.Messaging.Application
{
    public class Constants
    {
        public struct EventType
        {
            public const string EnterTheRoom = "enter-the-room";
            public const string LeaveTheRoom = "leave-the-room";
            public const string Comment = "comment";
            public const string HighFive = "high-five-another-user";
        }
    }
}