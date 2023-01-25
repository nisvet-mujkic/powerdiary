using FluentAssertions;
using PowerDiary.Messaging.Application.Extensions;
using Xunit;

namespace PowerDiary.Messaging.UnitTests.Extensions
{
    public class StringExtensionsUnitTests
    {
        [Theory]
        [InlineData("event", "events", 1, "event")]
        [InlineData("event", "events", 2, "events")]
        public void WordIsCorrectlyPluralized(string singular, string plural, int occurrence, string expected)
        {
            var word = singular.Pluralize(occurrence, plural);

            word.Should().Be(expected);
        }
    }
}
