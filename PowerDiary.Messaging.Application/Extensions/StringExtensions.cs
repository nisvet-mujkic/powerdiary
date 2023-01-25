namespace PowerDiary.Messaging.Application.Extensions
{
    public static class StringExtensions
    {
        public static string Pluralize(this string singular, int occurrence, string plural)
        {
            if (string.IsNullOrWhiteSpace(singular))
                return string.Empty;

            if (string.IsNullOrWhiteSpace(plural))
                return singular;

            return occurrence == 1 ? singular : plural;
        }
            
    }
}