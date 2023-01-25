namespace PowerDiary.Messaging.Application.Extensions
{
    public static class StringExtensions
    {
        public static string Pluralize(this string singular, int occurrence, string plural) =>
            occurrence == 1 ? singular : plural;
    }
}