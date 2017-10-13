using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Missile.TextLauncher.Interpretation.Lexing;
using Missile.TextLauncher.Interpretation.Parsing;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    [ExcludeFromCodeCoverage]
    public class Node_Should
    {
        [Fact]
        public void Correctly_Calculate_Hashes()
        {
            var p = new ProviderNode(new ProviderToken("noop", new[] {"arg1", "arg2"}));
            var p2 = new ProviderNode(new ProviderToken("noop", new[] {"arg1", "arg2"}));
            p.GetHashCode().Should().Be(p2.GetHashCode(),
                "nodes with the same property values should have the same hash code");
        }
    }
}