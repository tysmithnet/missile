using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Missile.TextLauncher.Destination;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class RegisteredDestination_Should
    {
        [Fact]
        public void Provide_Correct_Equality_Logic()
        {
            var dest = new ConsoleDestination();
            new RegisteredDestination(dest, typeof(IDestination<object>)).Equals(
                new RegisteredDestination(dest, typeof(IDestination<object>))).Should().BeTrue();
            new RegisteredDestination(dest, typeof(IDestination<object>)).GetHashCode().Equals(
                new RegisteredDestination(dest, typeof(IDestination<object>)).GetHashCode()).Should().BeTrue();

            new RegisteredDestination(dest, typeof(IDestination<object>)).Equals(
                new RegisteredDestination(dest, typeof(IDestination<string>))).Should().BeFalse();
            new RegisteredDestination(dest, typeof(IDestination<object>)).GetHashCode().Equals(
                new RegisteredDestination(dest, typeof(IDestination<string>)).GetHashCode()).Should().BeFalse();
        }
    }
}