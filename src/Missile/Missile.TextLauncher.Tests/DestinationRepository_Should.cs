using FluentAssertions;
using Missile.TextLauncher.Destination;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    public class DestinationRepository_Should
    {
        [Fact]
        public void Create_RegisteredDestination_Correctly()
        {
            var repo = new DestinationRepository();
            var consoleDestination = new ConsoleDestination();
            repo.Destinations = new[] {consoleDestination};
            var expected = new RegisteredDestination(consoleDestination, typeof(IDestination<object>));
            repo.RegisteredDestinations.Should().Equal(expected);
        }
    }
}